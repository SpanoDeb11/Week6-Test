using AcademyG.Week6.Test.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyG.Week6.Test.Core.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetByOrderCode(string code);
    }
}
