using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CorePorject.IRepository;
using CorePorject.IService;

namespace CorePorject.Service
{
    public class BaseService<T>:IBaseService<T> where T:class ,new()
    {
        protected readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            this._repository = repository;
        }
        public void Add(T t)
        {
            _repository.Add(t);
        }

        public void Delete(T t)
        {
            _repository.Delete(t);
        }

        public void Update(T t)
        {
            _repository.Update(t);
        }

        public List<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            return _repository.GetModels(whereLambda).ToList();
        }

        public List<T> GetModelsByPage<type>(int pageIndex, int pageSize, bool isAsc, Expression<Func<T, type>> orderByLambda, Expression<Func<T, bool>> whereLambda)
        {
            return _repository.GetModelsByPage<type>(pageIndex, pageSize, isAsc, orderByLambda, whereLambda).ToList();
        }

        public List<T> GetModelsByPageList<type>(int pageIndex, int pageSize, bool isAsc, Expression<Func<T, type>> orderByLambda, Expression<Func<T, bool>> whereLambda, out int count)
        {
            return _repository.GetModelsByPageList<type>(pageIndex, pageSize, isAsc, orderByLambda, whereLambda,out count).ToList();
        }

        public int SaveChanges()
        {
            return _repository.SaveChanges();
        }

        
    }
}
