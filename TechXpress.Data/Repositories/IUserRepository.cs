using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.Data.Entities;

namespace TechXpress.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public interface IUserRepository : IRepository<User>
        {
            Task<User?> GetUserByEmailAsync(string email);
        }
    }
}
