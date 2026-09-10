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
        private readonly IChatMessageRepo _chatRepo;
        private readonly IConversationRepo _convRepo;

        public CreateConversationCommandHandler(

            IChatNotifier chatNotifier, IChatMessageRepo chatRepo, IConversationRepo convRepo)
        {

            _chatNotifier = chatNotifier;
            _chatRepo = chatRepo;
            _convRepo = convRepo;
        }

        public async Task<int> Handle(
            CreateConversationCommand request,
            CancellationToken cancellationToken)
        {
            var conversation = new Domain.Entities.Conversation
            {
                CustomerId = 1,

                Status = ConversationStatus.Waiting,

                CreatedAt = DateTime.UtcNow
            };

            await _convRepo.AddConversationAsync(conversation);


            // Notify all connected agents
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
