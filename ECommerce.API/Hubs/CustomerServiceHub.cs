using ECommerce.Application.Conversation.Commands.CreateConversation;
using ECommerce.Application.Conversation.Commands.SendMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ECommerce.API.Hubs;

[Authorize]
public class CustomerServiceHub : Hub
{
    private readonly IMediator _mediator;

    public CustomerServiceHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<int> CreateConversation(
        int customerId)
    {
        return await _mediator.Send(
            new CreateConversationCommand(customerId));
    }

    public async Task JoinConversation(
        int conversationId)
    {
        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            $"conversation:{conversationId}");
    }

    public async Task SendMessage(
        int customerId,
        int conversationId,
        string content)
    {
        await _mediator.Send(
            new SendMessageCommand(
                customerId,
                conversationId,
                content));
    }
}