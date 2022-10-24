using Application.Interfaces;
using Domain.Models.Entities;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence.Repository;

public class UserRepository :GenericRepository<User>,IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}