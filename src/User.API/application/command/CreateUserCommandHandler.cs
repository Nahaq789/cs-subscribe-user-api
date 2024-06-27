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
    private readonly IMediator _mediator;
    private readonly ICryptoPasswordService _cryptoPasswordService;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="userRepository">ユーザーリポジトリ</param>
    public CreateUserCommandHandler(IUserRepository userRepository, IMediator mediator, ICryptoPasswordService cryptoPasswordService)
    {
        this._userRepository = userRepository;
        this._mediator = mediator;
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
        var userAggregate = new UserAggregate(Guid.NewGuid());
        var cryptoRecord = await Task.Run(() => _cryptoPasswordService.HashPassword(command.Password));

        userAggregate.setUser(command.Name, command.Email, cryptoRecord.Password, command.Age, userAggregate.UserAggregateId);
        userAggregate.setSalt(cryptoRecord.Salt, userAggregate.UserAggregateId);
        await _userRepository.CreateAsync(userAggregate);
        return await _userRepository.UnitOfWork.SaveEntityAsync(cancellationToken);
    }
}