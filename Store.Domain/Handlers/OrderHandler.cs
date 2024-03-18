using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Handlers
{
    public class OrderHandler : Notifiable<Notification>, IHandler<CreateOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryfeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderHandler(
            ICustomerRepository customerRepository,
            IDeliveryFeeRepository deliveryfeeRepository,
            IDiscountRepository discountRepository,
            IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _customerRepository = customerRepository;
            _deliveryfeeRepository = deliveryfeeRepository;
            _discountRepository = discountRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public ICommandResult Handle(CreateOrderCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
