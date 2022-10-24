namespace Application.Interfaces;

public interface IGenericRepository<T> where T : class
{
    List<T> Entities { get; }

    IEnumerable<T> GetAll();

    T GetById(object id);

    void Insert(T obj);

    void Update(T obj);

    void Delete(object id);

    Task Save();

    Task<T> AddAsync(T entity);
}