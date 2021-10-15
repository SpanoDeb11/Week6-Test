using AcademyG.Week6.Test.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyG.Week6.Test.Core.BL
{
    public interface IMainBusinessLayer
    {
        #region CRUD Client

        IList<Client> GetAllClients();
        Client GetClientById(int id);
        Client GetClientByCode(string code);
        bool AddNewClient(Client newClient);
        bool UpdateClient(Client client);
        bool DeleteClientById(int id);

        #endregion

        #region CRUD Order

        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        Order GetOrderByCode(string code);
        bool AddNewOrder(Order newOrder);
        bool UpdateOrder(Order order);
        bool DeleteOrderById(int id);

        #endregion
    }
}
