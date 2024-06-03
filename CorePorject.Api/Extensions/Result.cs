using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorePorject.Api
{
    public class ResultHandle
    {
        public int state { get; set; }
        public string code { get; set; }
        public string msg { get; set; }
    }
    public class ResultData<T>
    {
        public int state { get; set; }
        public string code { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
    }
    public class ResultPageList<T>
    {
        public int state { get; set; }
        public string code { get; set; }
        public string msg { get; set; }
        public int page { get; set; }
        public int size { get; set; }
        public int count { get; set; }
        public List<T> data { get; set; }
    }
}
