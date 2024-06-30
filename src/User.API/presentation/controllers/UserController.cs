using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using User.API.application.command;
using User.API.application.extensions.attribute;

namespace User.API.application.presentation.controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class UserController
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;

    public UserController(IMediator mediator, ILogger<UserController> logger)
    {
        this._mediator = mediator;
        this._logger = logger;
    }

    /// <summary>
    /// ユーザー作成のエンドポイントです
    /// </summary>
    ///<param name="command">ユーザー作成コマンド</param>
    ///<param name="requestId">リクエストID</param>
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> CreateUserAsync(
        [FromHeader(Name = "x-requestId")] Guid requestId,
        [FromBody] CreateUserCommand command
    )
    {
        if (requestId == Guid.Empty)
        {
            return TypedResults.BadRequest("リクエストIDが無効です");
        }
        var result = await _mediator.Send(command);
        if (!result)
        {
            return TypedResults.Problem(detail: "ユーザー作成に失敗しました。もう一度やり直してください。", statusCode: 500);
        }

        return TypedResults.Ok();
    }

    /// <summary>
    /// ユーザー内容変更時のエンドポイントです
    /// </summary>
    ///<param name="command">ユーザーアップデートコマンド</param>
    ///<param name="requestId">リクエストID</param>
    [HttpPost("update")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> UpdateUserAsync(
        [FromHeader(Name = "x-requestId")] Guid requestId,
        [FromBody] UserUpdateCommand command
    )
    {
        if (requestId == Guid.Empty)
        {
            return TypedResults.BadRequest("リクエストIDが無効です");
        }
        try
        {
            _logger.LogInformation($"Sending Command {command}");
            var result = await _mediator.Send(command);

            if (!result)
            {
                return TypedResults.Problem(detail: "ユーザーの変更に失敗しました。", statusCode: 500);
            }
            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(detail: ex.InnerException?.Message ?? ex.Message, statusCode: 500);
        }

    }
}