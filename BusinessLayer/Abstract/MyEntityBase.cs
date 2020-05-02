﻿using Core;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public class MyEntityBase<T> : IDataAccess<T> where T : class
    {
        Repository<T> repo = new Repository<T>();

        public virtual int Delete(T obj)
        {
           return repo.Delete(obj);
        }

        public virtual T Find(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return repo.Find(where);
        }

        public virtual int Insert(T obj)
        {
           return repo.Insert(obj);
        }

        public virtual List<T> List()
        {
            return repo.List();
        }

        public virtual List<T> List(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return repo.List(where);
        }

        public virtual IQueryable<T> ListQueryable()
        {
            return repo.ListQueryable();
        }

        public virtual int Save()
        {
            return repo.Save();
        }

        public virtual int Update(T obj)
        {
            return repo.Update(obj);
        }
    }
}
