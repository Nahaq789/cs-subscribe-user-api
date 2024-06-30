using MediatR;
using User.API.application.service;
using User.domain.model;
using User.Domain.exceptions;

namespace User.API.application.command;

/// <summary>
/// ユーザー作成コマンドハンドラーです。
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly ICryptoPasswordService _cryptoPasswordService;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="userRepository">ユーザーリポジトリ</param>
    /// /// <param name="cryptoPasswordService">パスワードサービス</param>
    public CreateUserCommandHandler(IUserRepository userRepository, ICryptoPasswordService cryptoPasswordService)
    {
        this._userRepository = userRepository;
        this._cryptoPasswordService = cryptoPasswordService;
    }

    /// <summary>
    /// ユーザー作成コマンドを処理します。
    /// </summary>
    /// <param name="command">ユーザー作成コマンド。</param>
    /// <param name="cancellationToken">キャンセレーショントークン。</param>
    /// <returns>真偽値</returns>
    public async Task<bool> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        await EnsureEmailIsUnique(command.Email);

        var userAggregate = new UserAggregate(Guid.NewGuid());
        var cryptoRecord = await Task.Run(() => _cryptoPasswordService.HashPassword(command.Password));

        userAggregate.setUser(command.Name, command.Email, cryptoRecord.Password, command.Age, userAggregate.UserAggregateId);
        userAggregate.setSalt(cryptoRecord.Salt, userAggregate.UserAggregateId);
        await _userRepository.CreateAsync(userAggregate);
        return await _userRepository.UnitOfWork.SaveEntityAsync(cancellationToken);
    }

    private async Task EnsureEmailIsUnique(string email)
    {
        var existingUser = await _userRepository.FindUserByEmail(email);
        if (existingUser != null && existingUser.User?.Email != null)
        {
            throw new UserDomainException("メールアドレスはすでに存在しています。");
        }
    }
}