﻿using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            //Get Basket from repo.
            var basket = await _basketRepo.GetBasetAsync(basketId);

            //Get Item themselves from the repo.
            var items = new List<OrderItem>();
            foreach(var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureURL);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity); //Getting Price directly from DB instead of Client.
                items.Add(orderItem);
            }

            //Get DeliveryMethods from repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //Calculate Subtotal.
            var subTotal = items.Sum(item => item.Price * item.Quantity);

            //Create Order
            var order = new Order(items, buyerEmail, shippingAddress,deliveryMethod, subTotal);
            _unitOfWork.Repository<Order>().Add(order);

            //Save to DB(Database).
            var result = await _unitOfWork.Complete();
            if(result <= 0) return null;

            //Delete basket
            await _basketRepo.DeleteBasketAsync(basketId);


            //Return Order.
            return order;

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrderWithItemsAndOrderingSpecification(id, buyerEmail);
            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemsAndOrderingSpecification(buyerEmail);
            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}
