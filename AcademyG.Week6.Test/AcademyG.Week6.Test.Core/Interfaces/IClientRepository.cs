using AcademyG.Week6.Test.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyG.Week6.Test.Core.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Client GetByClientCode(string code);
    }
}
