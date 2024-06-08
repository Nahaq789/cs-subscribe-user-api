namespace User.domain.model;

/// <summary>
/// ユーザーリポジトリです。
/// </summary>
public interface IUserRepository
{
    Task CreateUser(UserEntity userEntity);
    Task UpdateUser(UserEntity userEntity);
}