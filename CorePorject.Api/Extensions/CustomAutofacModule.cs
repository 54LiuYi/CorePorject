using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CorePorject.Api
{
    public class CustomAutofacModule : Autofac.Module
    {
        /// <summary>
        /// AutoFac注册类
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            List<Type> types = new List<Type>();
            var svrNames = new string[] { "CorePorject.Repository", "CorePorject.Service" };  //程序及名称
            foreach (string item in svrNames)
            {
                // 加载接口服务实现层。
                Assembly SvrAss = Assembly.Load(item);
                // 反射扫描这个程序集中所有的类，得到这个程序集中所有类的集合。
                types.AddRange(SvrAss.GetTypes());
            }
            // 告诉AutoFac容器，创建stypes这个集合中所有类的对象实例  在一次Http请求上下文中,共享一个组件实例
            builder.RegisterTypes(types.ToArray())
                        .AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();
            //指明创建的stypes这个集合中所有类的对象实例，以其接口的形式保存
        }
    }
}
