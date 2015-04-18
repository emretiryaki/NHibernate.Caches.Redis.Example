using System;
using NHibernate.Caches.Redis.Example.Entities;
using NHibernate.Linq;
using NHibernate.SqlCommand;
using ServiceStack.Redis;
using StackExchange.Redis;

namespace NHibernate.Caches.Redis.Example
{
    class Program 
    {
        protected const string ValidHost = "localhost:6379";
        protected static BasicRedisClientManager ClientManager { get; private set; }
        protected static IRedisClient Redis { get; private set; }
        protected static IRedisNativeClient RedisNative { get { return (IRedisNativeClient)Redis; } }

        static void Main(string[] args)
        {
            var clientManager = new BasicRedisClientManager("localhost:6379");

            using (var client = clientManager.GetClient())
            {
                client.FlushDb();
            }
           RedisCacheProvider.SetClientManager(clientManager);
           
            // ClientManager = new BasicRedisClientManager(ValidHost);
            //Redis = ClientManager.GetClient();
            //Redis.FlushDb();
           

        
            using (var session = NHibernateSessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var DepartmentObject = new Department { Name = "IT", PhoneNumber = "962788700227" };
                    session.Save(DepartmentObject);
                    transaction.Commit();
                    Console.WriteLine("Department Created: " + DepartmentObject.Name);
                    Console.ReadLine();

                    Department departmentAlias = null;
                    var query  = session.QueryOver<Employee>()
                        .JoinAlias(x => x.EmployeeDepartment, () => departmentAlias, JoinType.LeftOuterJoin).Cacheable();
                   var c= query.List();

                  var d=  query.List();

                    Console.WriteLine("Employee Listed: " + query.SingleOrDefault().FirstName);
                    Console.ReadLine();
                }
            }
            using (var session = NHibernateSessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                   
                    Department departmentAlias = null;
                    var query = session.QueryOver<Employee>()
                        .JoinAlias(x => x.EmployeeDepartment, () => departmentAlias, JoinType.LeftOuterJoin).Cacheable();
                    var c = query.List();

                    var d = query.List();

                    Console.WriteLine("Employee Listed: " + query.SingleOrDefault().FirstName);
                    Console.ReadLine();
                }
            }
        }

       
    }
}
