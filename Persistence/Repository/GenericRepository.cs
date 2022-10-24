using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    protected GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<T> Entities => _context.Set<T>().ToList();

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T GetById(object id)
    {
        return _context.Set<T>().Find(id);
    }

    public void Insert(T obj)
    {
        _context.Set<T>().Add(obj);
    }

    public void Update(T obj)
    {
        _context.Set<T>().Attach(obj);
        _context.Entry(obj).State = EntityState.Modified;
    }

    public void Delete(object id)
    {
        T existing = _context.Set<T>().Find(id);
        _context.Set<T>().Remove(existing);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }
}