using MediatR;
using User.API.application.service;
using User.domain.model;

namespace User.API.application.command;

/// <summary>
/// ユーザー作成コマンドハンドラーです。
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly ICryptoPasswordService _cryptoPasswordService;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="userRepository">ユーザーリポジトリ</param>
    public CreateUserCommandHandler(IUserRepository userRepository, ICryptoPasswordService cryptoPasswordService) {
        this._userRepository = userRepository;
        this._cryptoPasswordService = cryptoPasswordService;
    }

    /// <summary>
    /// ユーザー作成コマンドを処理します。
    /// </summary>
    /// <param name="command">ユーザー作成コマンド。</param>
    /// <param name="cancellationToken">キャンセレーショントークン。</param>
    /// <returns>作成されたユーザーのID。</returns>
    public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var salt = _cryptoPasswordService.CreateSalt();
        var User = new UserEntity(
            command.UserId, 
            command.Email, 
            _cryptoPasswordService.HashPassword(command.Password, salt), 
            command.Age);
        
        await _userRepository.CreateUser(User);
        return Guid.NewGuid();
    }
}