using AcademyG.Week6.Test.Core;
using AcademyG.Week6.Test.Core.BL;
using AcademyG.Week6.Test.Core.EF.Repositories;
using AcademyG.Week6.Test.Core.Entities;
using AcademyG.Week6.Test.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AcademyG.Week6.Test.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ManagementService : IManagementService
    {
        private readonly IMainBusinessLayer _mainBL;

        public ManagementService()
        {
            DependencyContainer.Register<IMainBusinessLayer, MainBusinessLayer>();
            DependencyContainer.Register<IClientRepository, EFClientRepository>();
            DependencyContainer.Register<IOrderRepository, EFOrderRepository>();

            this._mainBL = DependencyContainer.Resolve<IMainBusinessLayer>();
        }

        public bool AddNewClient(Client newClient)
        {
            if (newClient == null)
                return false;

            if (this._mainBL.GetClientByCode(newClient.ClientCode) != null)
                return false;

            return this._mainBL.AddNewClient(newClient);
        }

        public bool DeleteClientById(int id)
        {
            if (id <= 0)
                return false;

            if (this._mainBL.GetClientById(id) == null)
                return false;

            return this._mainBL.DeleteClientById(id);
        }

        public IList<Client> GetAllClients()
        {
            return this._mainBL.GetAllClients();
        }

        public Client GetClientByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;

            return this._mainBL.GetClientByCode(code);
        }

        public Client GetClientById(int id)
        {
            if (id <= 0)
                return null;

            return this._mainBL.GetClientById(id);
        }

        public bool UpdateClient(Client client, int id)
        {
            if (client == null)
                return false;

            if (client.Id != id)
                return false;

            return this._mainBL.UpdateClient(client);


        }
    }
}
