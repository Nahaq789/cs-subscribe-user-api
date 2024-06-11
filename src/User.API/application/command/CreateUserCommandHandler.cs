using MediatR;
using User.domain.model;

namespace User.API.application.command;

/// <summary>
/// ユーザー作成コマンドハンドラーです。
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="userRepository">ユーザーリポジトリ</param>
    public CreateUserCommandHandler(IUserRepository userRepository) => this._userRepository = userRepository;

    /// <summary>
    /// ユーザー作成コマンドを処理します。
    /// </summary>
    /// <param name="request">ユーザー作成コマンド。</param>
    /// <param name="cancellationToken">キャンセレーショントークン。</param>
    /// <returns>作成されたユーザーのID。</returns>
    /// <exception cref="System.NotImplementedException">まだ実装されていません。</exception>
    public Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}