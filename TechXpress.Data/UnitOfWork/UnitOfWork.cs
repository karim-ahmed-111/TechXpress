using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.Data.Context;
using TechXpress.Data.Repositories;

namespace TechXpress.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TechXpressContext _dbContext;

        public IUserRepository Users { get; }
        public UnitOfWork(TechXpressContext context)
        {
            _dbContext = context;
            Users = new UserRepository(_dbContext);
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
