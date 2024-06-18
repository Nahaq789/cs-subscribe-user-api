using MediatR;
using User.API.application.service;
using User.domain.model;

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
    /// <returns>作成されたユーザーのID。</returns>
    public async Task<bool> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var userAggregate = new UserAggregate(Guid.NewGuid());
        var salt = _cryptoPasswordService.CreateSalt();

        userAggregate.setUser(command.Name, command.Email, command.Password, command.Age, userAggregate.UserAggregateId);
        userAggregate.setSalt(salt, userAggregate.UserAggregateId);
        await _userRepository.CreateUser(userAggregate);
        return await _userRepository.UnitOfWork.SaveEntityAsync(cancellationToken);
    }
}