using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.Data.Context;
using TechXpress.Data.Entities;

namespace TechXpress.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly TechXpressContext _context;

        public UserRepository(TechXpressContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
