using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities;

public class Conversation
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int? AgentId { get; set; }

    public ConversationStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public Customer Customer { get; set; } = null!;

    public ICollection<ChatMessage> Messages { get; set; }
        = new List<ChatMessage>();
}