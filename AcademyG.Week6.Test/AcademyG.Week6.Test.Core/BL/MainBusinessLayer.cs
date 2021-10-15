using AcademyG.Week6.Test.Core.Entities;
using AcademyG.Week6.Test.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcademyG.Week6.Test.Core.BL
{
    public class MainBusinessLayer : IMainBusinessLayer
    {
        private readonly IClientRepository _clientRepository;
        private readonly IOrderRepository _orderRepository;

        public MainBusinessLayer() // chiamato da WCF
        {
            this._clientRepository = DependencyContainer.Resolve<IClientRepository>();
            this._orderRepository = DependencyContainer.Resolve<IOrderRepository>();
        }

        public MainBusinessLayer(IClientRepository clientRepo, IOrderRepository orderRepo) // chiamato da asp.net api
        {
            this._clientRepository = clientRepo;
            this._orderRepository = orderRepo;
        }

        #region BL Client
        public IList<Client> GetAllClients()
        {
            return this._clientRepository.Fetch().ToList();
        }

        public Client GetClientById(int id)
        {
            if (id <= 0)
                return null;

            return this._clientRepository.GetById(id);
        }

        public Client GetClientByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;

            return this._clientRepository.GetByClientCode(code);
        }

        public bool AddNewClient(Client newClient)
        {
            if (newClient == null)
                return false;

            if (this._clientRepository.GetByClientCode(newClient.ClientCode) != null)
                return false;

            return this._clientRepository.Add(newClient);
        }

        public bool UpdateClient(Client client)
        {
            if (client == null)
                return false;

            return this._clientRepository.Update(client);
        }

        public bool DeleteClientById(int id)
        {
            if (id <= 0)
                return false;

            if (this._clientRepository.GetById(id) == null)
                return false;

            return this._clientRepository.DeleteById(id);
        }

        #endregion

        #region BL Order
        public List<Order> GetAllOrders()
        {
            return this._orderRepository.Fetch().ToList();
        }

        public Order GetOrderById(int id)
        {
            if (id <= 0)
                return null;

            return this._orderRepository.GetById(id);
        }

        public Order GetOrderByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;

            return this._orderRepository.GetByOrderCode(code);
        }

        public bool AddNewOrder(Order newOrder)
        {
            if (newOrder == null)
                return false;

            if (this._orderRepository.GetByOrderCode(newOrder.OrderCode) != null)
                return false;

            Client client = this._clientRepository.GetById(newOrder.ClientId);

            if (client == null)
                return false;

            newOrder.Client = client;

            return this._orderRepository.Add(newOrder);
        }

        public bool UpdateOrder(Order order)
        {
            if (order == null)
                return false;

            var item = this._orderRepository.GetById(order.Id);
            if (item == null)
                return false;

            order.ClientId = item.ClientId;

            return this._orderRepository.Update(order);
        }

        public bool DeleteOrderById(int id)
        {
            if (id <= 0)
                return false;

            if (this._orderRepository.GetById(id) == null)
                return false;

            return this._orderRepository.DeleteById(id);
        }

        #endregion
    }
}
