using AcademyG.Week6.Test.Core.Entities;
using AcademyG.Week6.Test.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcademyG.Week6.Test.Core.EF.Repositories
{
    public class EFClientRepository : IClientRepository
    {
        private readonly ManagementContext _ctx;

        public EFClientRepository()
        {
            this._ctx = new ManagementContext();
        }

        public EFClientRepository(ManagementContext ctx)
        {
            this._ctx = ctx;
        }

        public bool Add(Client newItem)
        {
            if (newItem == null)
                return false;

            try
            {
                if (this.GetByClientCode(newItem.ClientCode) != null)
                    return false;

                _ctx.Clients.Add(newItem);
                _ctx.SaveChanges();
                return true;
            }
            catch(Exception ex)
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
                Client deleted = this.GetById(id);

                if (deleted == null)
                    return false;

                _ctx.Clients.Remove(deleted);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Client> Fetch(Func<Client, bool> filter = null)
        {
            try
            {
                if (filter != null)
                    return _ctx.Clients.Where(filter);

                return _ctx.Clients;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
                return null;
            }
        }

        public Client GetByClientCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;

            try
            {
                var result = this._ctx.Clients.Where(c => c.ClientCode == code);
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

        public Client GetById(int id)
        {
            if (id <= 0)
                return null;

            try
            {
                return _ctx.Clients.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
                return null; 
            }
        }

        public bool Update(Client updatedItem)
        {
            if (updatedItem == null)
                return false;

            try
            {
                this._ctx.Clients.Update(updatedItem);
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
