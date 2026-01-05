using Application.Modules.R2Y.Repositories.Interface.Chat;
using FluentResults;
using StrawberryShake;

namespace Application.Modules.R2Y.Repositories.Service.Chat;

public class ChatService(IR2YGqlClient client) : IChatService
{
    public async Task<Result<IReadOnlyList<IGetAllUserRoom_AllChatRoomByUserEmail>?>> GetAllUserRoom(string currentUserEmail,
        CancellationToken cancellationToken = default)
    {
        return await GetAllUserRoom(cancellationToken);
    }

    public async Task<Result<IReadOnlyList<IGetAllUserRoom_AllChatRoomByUserEmail>?>> GetAllUserRoom(CancellationToken cancellationToken = default)
    {
        var response = await client.GetAllUserRoom.ExecuteAsync(cancellationToken);

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
            Content = content
        });

        return result.IsErrorResult() ? Result.Fail(result.Errors.Select(e => e.Message)) : Result.Ok(result.Data?.SendChatMessage!);
    }

    public async Task<Result<IGetChatMessages_AllMessagesByChatRoomId>> GetMessagesAsync(Guid roomId, int page, int pageSize)
    {
        var result = await client.GetChatMessages.ExecuteAsync(roomId, page, pageSize);

        if (result.IsErrorResult())
        {
            return Result.Fail(result.Errors.Select(e => e.Message));
        }

        return Result.Ok(result.Data?.AllMessagesByChatRoomId!);
    }

    public async Task<Result<bool>> DeleteMessageAsync(Guid chatRoomId, Guid messageId, string userEmail)
    {
        var result = await client.DeleteChatMessage.ExecuteAsync(new DeleteMessageInput
        {
            ChatRoomId = chatRoomId,
            MessageId = messageId,
        });

        if (result.IsErrorResult())
        {
            return Result.Fail(result.Errors.Select(e => e.Message));
        }

        return Result.Ok(result.Data?.DeleteChatMessage?.Success ?? false);
    }

    public IObservable<IOperationResult<IOnChatEventResult>> SubscribeToMessages(string currentUserEmail)
    {
        var result = client.OnChatEvent.Watch();
        return result;
    }
}
