using MediatR;

namespace ECommerce.Application.Conversation.Commands.CreateConversation
{
    public record CreateConversationCommand
    : IRequest<int>;
}
