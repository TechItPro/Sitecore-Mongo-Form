using MongoDB.Driver;
using System.Collections.Generic;

namespace TechITPro.Samples.DAL
{
    public interface IBaseRepository<T>
    {
        string DataBaseName { get; set; }
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> Select(IMongoQuery query);
        IEnumerable<T> SelectAll();
    }
}
