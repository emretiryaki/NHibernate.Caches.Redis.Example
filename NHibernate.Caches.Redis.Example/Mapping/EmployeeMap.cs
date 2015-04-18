using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernate.Caches.Redis.Example.Entities;

namespace NHibernate.Caches.Redis.Example.Mapping
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Id(x => x.Id);

            Map(x => x.FirstName);

            Map(x => x.Position);

            References(x => x.EmployeeDepartment).Column("DepartmentId");

            Table("Employee");

            Cache.ReadWrite();
        }
    }
}
