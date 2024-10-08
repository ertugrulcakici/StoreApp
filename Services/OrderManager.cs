using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderManager : IOrderService
    {
        private readonly IRepositoryManager _manager;

        public OrderManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public IQueryable<Order> Orders => _manager.OrderRepository.Orders;

        public int NUmberOfInProcess => _manager.OrderRepository.NUmberOfInProcess;

        public void Complete(int id)
        {
            _manager.OrderRepository.Complete(id);
            _manager.Save();

        }

        public Order? GetOneOrder(int id)
        {
            return _manager.OrderRepository.GetOneOrder(id);
        }

        public void SaveOrder(Order order)
        {
            _manager.OrderRepository.SaveOrder(order);
            _manager.Save();
        }
    }
}
