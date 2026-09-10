using ECommerce.API.Hubs;
using ECommerce.Application.Conversation.Dto;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ECommerce.API.SignalR
{
    public class ChatNotifier : IChatNotifier
    {
        private readonly IHubContext<CustomerServiceHub>
            _hubContext;

        public ChatNotifier(
            IHubContext<CustomerServiceHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageAsync(
            int conversationId,
            ChatMessageDto message)
        {
            await _hubContext
                .Clients
                .Group($"conversation:{conversationId}")
                .SendAsync(
                    "ReceiveMessage",
                    message);
        }

        public async Task NotifyNewConversationAsync(
            ConversationDto conversation)
        {
            await _hubContext
                .Clients
                .Group("agents")
                .SendAsync(
                    "NewConversation",
                    conversation);
        }

        public async Task NotifyConversationAcceptedAsync(
            int conversationId,
            int agentId)
        {
            await _hubContext
                .Clients
                .Group($"conversation:{conversationId}")
                .SendAsync(
                    "ConversationAccepted",
                    new
                    {
                        conversationId,
                        agentId
                    });
        }

        public async Task NotifyMessageReadAsync(
            int conversationId,
            int messageId)
        {
            await _hubContext
                .Clients
                .Group($"conversation:{conversationId}")
                .SendAsync(
                    "MessageRead",
                    messageId);
        }

        public async Task NotifyConversationClosedAsync(
            int conversationId)
        {
            await _hubContext
                .Clients
                .Group($"conversation:{conversationId}")
                .SendAsync(
                    "ConversationClosed",
                    conversationId);
        }


    }
}
