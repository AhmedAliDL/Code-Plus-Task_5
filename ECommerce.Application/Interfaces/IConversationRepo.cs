namespace ECommerce.Application.Interfaces
{
    public interface IConversationRepo
    {
        Task<Domain.Entities.Conversation?> GetByIdAsync(int id);
        Task AddConversationAsync(Domain.Entities.Conversation conv);
    }
}
