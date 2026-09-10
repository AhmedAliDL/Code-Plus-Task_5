using ECommerce.Application.Conversation.Dto;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using MediatR;

namespace ECommerce.Application.Conversation.Commands.SendMessage
{
    public class SendMessageCommandHandler
    : IRequestHandler<SendMessageCommand, ChatMessageDto>
    {
        private readonly IChatNotifier _chatNotifier;
        private readonly IConversationRepo _convRepo;
        private readonly IChatMessageRepo _chatRepo;

        public SendMessageCommandHandler(
            IChatNotifier chatNotifier, IConversationRepo convRepo, IChatMessageRepo chatRepo)
        {
            _chatNotifier = chatNotifier;
            _convRepo = convRepo;
            _chatRepo = chatRepo;
        }

        public async Task<ChatMessageDto> Handle(
            SendMessageCommand request,
            CancellationToken cancellationToken)
        {
            var conversation = await _convRepo.GetByIdAsync(request.ConversationId);

            if (conversation is null)
                throw new KeyNotFoundException(
                    "Conversation not found.");

            if (conversation.Status != ConversationStatus.Active)
                throw new InvalidOperationException(
                    "Conversation is not active.");

            var userId = 1;

            if (conversation.CustomerId != userId &&
                conversation.AgentId != userId)
            {
                throw new UnauthorizedAccessException(
                    "You are not part of this conversation.");
            }

            var message = new ChatMessage
            {

                ConversationId = conversation.Id,

                SenderId = userId,

                Content = request.Content,

                SentAt = DateTime.UtcNow,

                IsRead = false
            };


            await _chatRepo.AddMessage(message);
            var dto = new ChatMessageDto
            {
                Id = message.Id,
                ConversationId = message.ConversationId,
                SenderId = message.SenderId,
                Content = message.Content,
                SentAt = message.SentAt,
                IsRead = message.IsRead
            };

            // Real-time notification
            await _chatNotifier.SendMessageAsync(
                conversation.Id,
                dto);

            return dto;
        }
    }
}
