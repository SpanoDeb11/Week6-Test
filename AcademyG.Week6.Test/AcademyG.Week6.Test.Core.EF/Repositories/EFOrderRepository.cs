using AcademyG.Week6.Test.Core.Entities;
using AcademyG.Week6.Test.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcademyG.Week6.Test.Core.EF.Repositories
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly ManagementContext _ctx;

        public EFOrderRepository()
        {
            this._ctx = new ManagementContext();
        }

        public EFOrderRepository(ManagementContext ctx)
        {
            this._ctx = ctx;
        }

        public bool Add(Order newItem)
        {
            if (newItem == null)
                return false;

            try
            {
                if (this.GetByOrderCode(newItem.OrderCode) != null)
                    return false;

                Client client = _ctx.Clients.Find(newItem.ClientId);
                if (client == null)
                    return false;

                newItem.Client = client;

                _ctx.Orders.Add(newItem);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            if (id <= 0)
                return false;
            try
            {
                Order deleted = this.GetById(id);

                if (deleted == null)
                    return false;

                _ctx.Orders.Remove(deleted);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Order> Fetch(Func<Order, bool> filter = null)
        {
            try
            {
                if (filter != null)
                    return _ctx.Orders.Include(o => o.Client).Where(filter);

                return _ctx.Orders.Include(o => o.Client);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
                return null;
            }
        }

        public Order GetById(int id)
        {
            if (id <= 0)
                return null;

            try
            {
                return _ctx.Orders.Include(o => o.Client).SingleOrDefault(o => o.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
                return null;
            }
        }

        public Order GetByOrderCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;

            try
            {
                var result = this._ctx.Orders.Where(c => c.OrderCode == code);
                if (result.Count() != 1)
                    return null;

                return result.First();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
                return null;
            }
        }

        public bool Update(Order updatedItem)
        {
            if (updatedItem == null)
                return false;

            try
            {
                this._ctx.Orders.Update(updatedItem);
                this._ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
                return false;
            }
        }
    }
}
