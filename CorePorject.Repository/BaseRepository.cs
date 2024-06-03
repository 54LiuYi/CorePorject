using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CorePorject.IRepository;
using Microsoft.Extensions.Configuration;

namespace CorePorject.Repository
{
    public class BaseRepository<T> :IBaseRepository<T> where T:class,new()
    {
        protected readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            this._context = context;
        }


        #region EF
        public void Add(T t)
        {
            _context.Set<T>().Add(t);
        }

        public void Delete(T t)
        {
            _context.Set<T>().Remove(t);
        }

        public void Update(T t)
        {
            _context.Set<T>().Update(t);
        }

        public IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda)
        {
            return _context.Set<T>().Where(whereLambda).AsQueryable<T>();
        }

        public IQueryable<T> GetModelsByPage<type>(int pageIndex, int pageSize,  bool isAsc, Expression<Func<T, type>> orderByLambda, Expression<Func<T, bool>> whereLambda)
        {
            if (isAsc)
                return _context.Set<T>()
                    .Where(whereLambda)
                    .OrderBy(orderByLambda)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .AsQueryable<T>();
            else
                return _context.Set<T>().Where(whereLambda)
                    .OrderByDescending(orderByLambda)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .AsQueryable<T>();

        }

        public IQueryable<T> GetModelsByPageList<type>(int pageIndex, int pageSize, bool isAsc, Expression<Func<T, type>> orderByLambda, Expression<Func<T, bool>> whereLambda, out int count)
        {
            var datas = _context.Set<T>().Where(whereLambda);
            count = datas.Count();
            if (isAsc)
                return datas
                    .OrderBy(orderByLambda)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .AsQueryable<T>();
            else
                return datas
                    .OrderByDescending(orderByLambda)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .AsQueryable<T>();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }


        #endregion

        #region Dapper
        public IEnumerable<dynamic> QueryDynamic(string sql, object param = null)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
