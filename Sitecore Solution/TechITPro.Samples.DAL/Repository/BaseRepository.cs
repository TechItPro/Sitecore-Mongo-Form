using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace TechITPro.Samples.DAL.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        #region Public Properties
        public virtual string DataBaseName { get; set; }
        public virtual string CollectionName { get; set; }
        public virtual string ConnectionString { get; set; }
        #endregion

        #region ..ctor
        public BaseRepository()
        {
        }
        public BaseRepository(string connectionString, string collectionName)
        {
        }
        #endregion

        #region Public Methods
        public virtual IEnumerable<T> Select(IMongoQuery query)
        {
            throw new NotImplementedException();
        }
        public virtual IEnumerable<T> SelectAll()
        {
            throw new NotImplementedException();
        }
        public virtual void Insert(T entity)
        {
            throw new NotImplementedException();
        }
        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }
        public virtual void Delete(T entity)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
