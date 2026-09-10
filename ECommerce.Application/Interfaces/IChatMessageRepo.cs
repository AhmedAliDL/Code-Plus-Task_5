using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IChatMessageRepo
    {
        Task AddMessage(ChatMessage message);
    }
}
