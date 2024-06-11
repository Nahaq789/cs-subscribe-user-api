using System.Runtime.Serialization;
using MediatR;
namespace User.API.application.command;

/// <summary>
/// ユーザ作成コマンドです。
/// </summary>
[DataContract]
public class CreateUserCommand : IRequest<string>
{
    [DataMember]
    public Guid UserId { get; private set; }
    [DataMember]
    public string Name { get; private set; }
    [DataMember]
    public string Email { get; private set; }
    [DataMember]
    public string Password { get; private set; }
    [DataMember]
    public int Age { get; private set; }
    [DataMember]
    public string Salt { get; private set; }

    public CreateUserCommand(Guid userId, string name, string email, string password, int age, string salt)
    {
        this.UserId = userId;
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.Age = age;
        this.Salt = salt;
    }
}
