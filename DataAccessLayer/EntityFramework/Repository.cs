using Common;
using Core;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class Repository<T> : RepositoryBase,IDataAccess<T> where T : class 
    {
        private DbSet<T> _objectSet;

        public Repository()
        {
            _objectSet = context.Set<T>();
        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);

            if(obj is MyEntityBase)
            {
                MyEntityBase meb = obj as MyEntityBase;
                DateTime now = DateTime.Now;

                meb.CreatedOn = now;
                meb.ModifiedOn = now;
                meb.ModifiedUsername = App.Common.GetCurrentUsername(); // TODO : bakılacak.
            }

            return Save();

        }

        public int Update(T obj)
        {
            if (obj is MyEntityBase)
            {
                MyEntityBase meb = obj as MyEntityBase;

                meb.ModifiedOn = DateTime.Now;
                meb.ModifiedUsername = App.Common.GetCurrentUsername(); // TODO : bakılacak.
            }

            return Save();
        }

        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public T Find(Expression<Func<T,bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }
    }
}
