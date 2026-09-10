using ECommerce.Application.Interfaces;
using ECommerce.DAL.Context;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL.Repositories
{
    public class ConversationRepo : IConversationRepo
    {
        public readonly AppDbContext _context;
        public ConversationRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Conversation?> GetByIdAsync(int id)
        {
            return await _context.Conversations
                .FirstOrDefaultAsync(
                    x => x.Id == id);
        }
        public async Task AddConversationAsync(Conversation conv)
        {
            await _context.Conversations.AddAsync(conv);
            await _context.SaveChangesAsync();
        }
    }


}
