using ECommerce.Domain.Enums;

namespace ECommerce.Application.Conversation.Dto
{
    public class ConversationDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int? AgentId { get; set; }

        public ConversationStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ClosedAt { get; set; }
    }
}
