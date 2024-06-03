using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace CorePorject.IRepository
{
    public interface IBaseRepository<T> where T:class
    {
        #region EF
        public void Add(T t);

        public void Delete(T t);

        public void Update(T t);

        public IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda);

        public IQueryable<T> GetModelsByPage<type>
            (int pageIndex, int pageSize, bool isAsc,
            Expression<Func<T, type>> orderByLambda,
            Expression<Func<T, bool>> whereLambda);

        public IQueryable<T> GetModelsByPageList<type>
            (int pageIndex, int pageSize, bool isAsc,
            Expression<Func<T, type>> orderByLambda,
            Expression<Func<T, bool>> whereLambda,out int count);

        public int SaveChanges();
        #endregion

        #region Dapper
        IEnumerable<dynamic> QueryDynamic(string sql, object param = null);
        #endregion
    }
}
