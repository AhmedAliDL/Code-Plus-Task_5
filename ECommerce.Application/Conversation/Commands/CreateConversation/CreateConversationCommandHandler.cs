using ECommerce.Application.Conversation.Dto;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using MediatR;

namespace ECommerce.Application.Conversation.Commands.CreateConversation
{
    public class CreateConversationCommandHandler
    : IRequestHandler<CreateConversationCommand, int>
    {
        private readonly IChatNotifier _chatNotifier;
        private readonly IConversationRepo _convRepo;

        public CreateConversationCommandHandler(
            IChatNotifier chatNotifier,
            IConversationRepo convRepo)
        {
            _chatNotifier = chatNotifier;
            _convRepo = convRepo;
        }

        public async Task<int> Handle(
            CreateConversationCommand request,
            CancellationToken cancellationToken)
        {
            var conversation = new Domain.Entities.Conversation
            {
                CustomerId = request.CustomerId,

                Status = ConversationStatus.Waiting,

                CreatedAt = DateTime.UtcNow
            };

            await _convRepo.AddConversationAsync(conversation);

            await _chatNotifier.NotifyNewConversationAsync(
                new ConversationDto
                {
                    Id = conversation.Id,
                    CustomerId = conversation.CustomerId,
                    Status = conversation.Status,
                    CreatedAt = conversation.CreatedAt
                });

            return conversation.Id;
        }
    }
}
