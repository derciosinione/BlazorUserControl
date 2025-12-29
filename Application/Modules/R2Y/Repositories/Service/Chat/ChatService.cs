using Application.Modules.R2Y.Repositories.Interface.Chat;
using FluentResults;
using StrawberryShake;

namespace Application.Modules.R2Y.Repositories.Service.Chat;

public class ChatService(IR2YGqlClient client) : IChatService
{
    public async Task<Result<IReadOnlyList<IGetAllUserRoom_AllChatRoomByUserEmail>?>> GetAllUserRoom(string currentUserEmail,
        CancellationToken cancellationToken = default)
    {
        var response = await client.GetAllUserRoom.ExecuteAsync(currentUserEmail, cancellationToken);

        if (response.IsSuccessResult())
        {
            var allChatRoomByUserEmail = response.Data!.AllChatRoomByUserEmail;
            return Result.Ok(allChatRoomByUserEmail)!;
        }

        var errors = response.Errors.Select(x => x.Message).ToList();
        return Result.Fail(errors);
    }

    public async Task<Result<ICreatePrivateRoom_CreatePrivateRoom>> CreatePrivateRoomAsync(string userEmail,
        string creatorEmail, CancellationToken cancellationToken = default)
    {
        var input = new CreatePrivateRoomInput
        {
            CreatorEmail = creatorEmail,
            UserEmail = userEmail,
        };

        var operationResult = await client.CreatePrivateRoom.ExecuteAsync(input, cancellationToken);

        if (operationResult.IsSuccessResult())
        {
            return Result.Ok(operationResult.Data!.CreatePrivateRoom);
        }

        var errors = operationResult.Errors.Select(x => x.Message).ToList();
        return Result.Fail(errors);
    }

    public async Task<Result<ISendChatMessage_SendChatMessage>> SendMessageAsync(Guid roomId, string senderEmail, string content)
    {
        var result = await client.SendChatMessage.ExecuteAsync(new CreateMessageInput
        {
            ChatRoomId = roomId,
            SenderEmail = senderEmail,
            Content = content
        });

        return result.IsErrorResult() ? Result.Fail(result.Errors.Select(e => e.Message)) : Result.Ok(result.Data?.SendChatMessage!);
    }

    public async Task<Result<IEnumerable<IGetChatMessages_AllMessagesByChatRoomId>>> GetMessagesAsync(Guid roomId)
    {
        var result = await client.GetChatMessages.ExecuteAsync(roomId);

        if (result.IsErrorResult())
        {
            return Result.Fail(result.Errors.Select(e => e.Message));
        }

        return Result.Ok(result.Data?.AllMessagesByChatRoomId ?? Enumerable.Empty<IGetChatMessages_AllMessagesByChatRoomId>());
    }

}
