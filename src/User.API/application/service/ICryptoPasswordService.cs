namespace User.API.application.service;

/// <summary>
/// ユーザのパスワードをハッシュ化するサービスです。
/// </summary>
public interface ICryptoPasswordService
{
    /// <summary>
    /// パスワードとソルトを使ってハッシュする
    /// </summary>
    /// <param name="password">パスワード</param>
    /// <param name="salt">ソルト</param>
    Task<(string Password, string Salt)> HashPassword(string password);

    /// <summary>
    /// ソルトを生成します。
    /// </summary>
    /// <param name="password">パスワード</param>
    /// <param name="salt">ソルト</param>
    string CreateSalt();
}