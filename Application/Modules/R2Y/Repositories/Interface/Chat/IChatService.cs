using FluentResults;
using StrawberryShake;

namespace Application.Modules.R2Y.Repositories.Interface.Chat;

public interface IChatService
{
    Task<Result<IGetChatMessages_AllMessagesByChatRoomId>> GetMessagesAsync(Guid roomId, int page, int pageSize);
    Task<Result<ISendChatMessage_SendChatMessage>> SendMessageAsync(Guid roomId, string senderEmail, string content);
    Task<Result<bool>> DeleteMessageAsync(Guid chatRoomId, Guid messageId, string userEmail);
    Task<Result<IReadOnlyList<IGetAllUserRoom_AllChatRoomByUserEmail>?>> GetAllUserRoom(string currentUserEmail,
        CancellationToken cancellationToken = default);

    Task<Result<ICreatePrivateRoom_CreatePrivateRoom>> CreatePrivateRoomAsync(string userEmail, string creatorEmail,
        CancellationToken cancellationToken = default);

    IObservable<IOperationResult<IOnMessageReceivedResult>> SubscribeToMessages(string currentUserEmail);
    // Task<Result<IEnumerable<ISearchChatUsers_AllUsers>>> SearchUsersAsync(string search);
    // Task<Result<IEnumerable<IGetChats_AllChatRoomByUserEmail>>> GetChatsAsync(string userEmail);

}
