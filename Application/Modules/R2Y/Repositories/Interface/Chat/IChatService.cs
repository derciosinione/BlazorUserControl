using FluentResults;

namespace Application.Modules.R2Y.Repositories.Interface.Chat;

public interface IChatService
{
    Task<Result<IEnumerable<IGetChatMessages_AllMessagesByChatRoomId>>> GetMessagesAsync(Guid roomId);
    Task<Result<ISendChatMessage_SendChatMessage>> SendMessageAsync(Guid roomId, string senderEmail, string content);
    Task<Result<IReadOnlyList<IGetAllUserRoom_AllChatRoomByUserEmail>?>> GetAllUserRoom(string currentUserEmail,
        CancellationToken cancellationToken = default);
    
    Task<Result<ICreatePrivateRoom_CreatePrivateRoom>> CreatePrivateRoomAsync(string userEmail, string creatorEmail,
        CancellationToken cancellationToken = default);
    // Task<Result<IEnumerable<ISearchChatUsers_AllUsers>>> SearchUsersAsync(string search);
    // Task<Result<IEnumerable<IGetChats_AllChatRoomByUserEmail>>> GetChatsAsync(string userEmail);

}
