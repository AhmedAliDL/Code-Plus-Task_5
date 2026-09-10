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

    public async Task SendMessage(
        int conversationId,
        string content)
    {
        await _mediator.Send(
            new SendMessageCommand(
                conversationId,
                content));
    }
}