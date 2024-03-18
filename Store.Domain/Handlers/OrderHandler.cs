using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories;
using Store.Domain.Utils;
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
            // Fail fast validation
            command.Validate();
            if (command.IsValid)
                return new GenericCommandResult(false,"Pedido inválido", command.Notifications);

            // recupera o cliente
            var customer = _customerRepository.Get(command.Customer);

            // calcula a taxa de entrega
            var deliveryFee = _deliveryfeeRepository.Get(command.ZipCode);

            // obtém o cupom de desconto
            var discount = _discountRepository.Get(command.PromoCode);

            // gera o pedido
            var products = _productRepository.Get(ExtractGuids.Extract(command.Items)).ToList();
            var order = new Order(customer, deliveryFee, discount);
            foreach(var item in command.Items)
            {
                var product = products.Where(x => x.Id == item.Product).FirstOrDefault();
                order.AddItem(product, item.Quantity);
            }

            // agrupar as notificações
            AddNotifications(order.Notifications);

            // verifica se deu tudo certo
            if (IsValid != true)
                return new GenericCommandResult(false, "Falha ao gerar o pedido", Notifications);

            // retornar o resultado
            _orderRepository.Save(order);
            return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso", order);
        }
    }
}
