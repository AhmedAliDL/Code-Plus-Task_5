using ECommerce.Application.Conversation.Dto;
using MediatR;

namespace ECommerce.Application.Conversation.Commands.SendMessage
{
    public record SendMessageCommand(
        int CustomerId,
        int ConversationId,
        string Content
    ) : IRequest<ChatMessageDto>;
}
