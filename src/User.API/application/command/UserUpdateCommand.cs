using MediatR;

namespace User.API.application.command;

public class UserUpdateCommand : IRequest<bool>
{
    public Guid AggregateId { get; private set; }
    public string Name { get; private set; }
    public int Age { get; private set; }

    public UserUpdateCommand(Guid aggregateId, string name, int age)
    {
        this.AggregateId = aggregateId;
        this.Name = name;
        this.Age = age;
    }
}