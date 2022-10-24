using Application.Interfaces;
using Domain.Models.Entities;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence.Repository
{
    public class StorageRepository : GenericRepository<Storage>, IStorageRepository
    {
        public StorageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}