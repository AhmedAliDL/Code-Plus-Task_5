using ECommerce.Application.Interfaces;
using ECommerce.DAL.Context;
using ECommerce.Domain.Entities;

namespace ECommerce.DAL.Repositories
{
    public class ChatMessageRepo : IChatMessageRepo
    {
        public readonly AppDbContext _context;
        public ChatMessageRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddMessage(ChatMessage message)
        {
            _context.ChatMessages.Add(message);

            await _context.SaveChangesAsync();
        }
    }
  
  
}
