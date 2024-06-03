using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CorePorject.IService
{
    public interface IBaseService<T>
    {
        public void Add(T t);

        public void Delete(T t);

        public void Update(T t);

        public List<T> GetList(Expression<Func<T, bool>> whereLambda);

        public List<T> GetModelsByPage<type>
            (int pageIndex, int pageSize, bool isAsc,
            Expression<Func<T, type>> orderByLambda,
            Expression<Func<T, bool>> whereLambda);

        public List<T> GetModelsByPageList<type>
            (int pageIndex, int pageSize, bool isAsc,
            Expression<Func<T, type>> orderByLambda,
            Expression<Func<T, bool>> whereLambda,out int count);

        public int SaveChanges();
    }
}
