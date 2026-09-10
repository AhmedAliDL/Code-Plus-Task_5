using ECommerce.Application.Conversation.Dto;

namespace ECommerce.Application.Interfaces
{
    public interface IChatNotifier
    {
        Task SendMessageAsync(
            int conversationId,
            ChatMessageDto message);

        Task NotifyNewConversationAsync(
            ConversationDto conversation);

        Task NotifyConversationAcceptedAsync(
            int conversationId,
            int agentId);

        Task NotifyMessageReadAsync(
            int conversationId,
            int messageId);

        Task NotifyConversationClosedAsync(
            int conversationId);
    }
}
