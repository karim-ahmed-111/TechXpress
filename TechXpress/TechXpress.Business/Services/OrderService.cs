using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.Business.DTOs;
using TechXpress.Data.Repositories;
using TechXpress.Domain.Entities;

namespace TechXpress.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;

        public OrderService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _uow.Orders.GetAllAsync();
            return orders.Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                OrderDate = o.OrderDate,
                Status = o.Status,
                Items = o.OrderItems?.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            });
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersForUserAsync(string userId)
        {
            var orders = await _uow.Orders.FindAsync(o => o.CustomerId == userId);
            return orders.Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                OrderDate = o.OrderDate,
                Status = o.Status,
                Items = o.OrderItems?.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            });
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var o = await _uow.Orders.GetByIdAsync(id);
            if (o == null) return null;
            return new OrderDto
            {
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                OrderDate = o.OrderDate,
                Status = o.Status,
                Items = o.OrderItems?.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }

        public async Task CreateOrderAsync(CreateOrderDto orderDto)
        {
            var order = new Order
            {
                CustomerId = orderDto.CustomerId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                OrderItems = new List<OrderItem>()
            };
            // Add items
            foreach (var item in orderDto.Items)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price// set from caller
                });
            }
            await _uow.Orders.AddAsync(order);
            await _uow.SaveAsync();
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _uow.Orders.GetByIdAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                _uow.Orders.Update(order);
                await _uow.SaveAsync();
            }
        }
    }
}
