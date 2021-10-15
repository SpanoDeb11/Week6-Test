using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyG.Week6.Test.Core.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> Fetch(Func<T, bool> filter = null);
        T GetById(int id);
        bool Add(T newItem);
        bool Update(T updatedItem);
        bool DeleteById(int id);
    }
}
