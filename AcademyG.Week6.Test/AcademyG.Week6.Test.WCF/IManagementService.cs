using AcademyG.Week6.Test.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AcademyG.Week6.Test.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IManagementService
    {
        [OperationContract]
        IList<Client> GetAllClients();

        [OperationContract]
        Client GetClientById(int id);

        [OperationContract]
        Client GetClientByCode(string code);

        [OperationContract]
        bool AddNewClient(Client newClient);

        [OperationContract]
        bool UpdateClient(Client client, int id);

        [OperationContract]
        bool DeleteClientById(int id);
    }
}
