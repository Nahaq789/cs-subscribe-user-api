using MediatR;
using User.domain.model;

namespace User.API.application.command;

public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public UserUpdateCommandHandler(IUserRepository userRepository)
    {
        this._userRepository = userRepository;
    }

    public async Task<bool> Handle(UserUpdateCommand command, CancellationToken cancellationToken)
    {
        var baseUser = await _userRepository.FindUserByAggregateId(command.AggregateId);
        if (baseUser == null)
        {
            throw new InvalidOperationException($"AggregateId: {command.AggregateId} not found");
        }

        baseUser.UpdateUserEntity(command.Name, command.Age);
        _userRepository.Update(baseUser);

        return await _userRepository.UnitOfWork.SaveEntityAsync(cancellationToken);
    }
}