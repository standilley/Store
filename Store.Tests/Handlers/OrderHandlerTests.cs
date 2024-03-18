using Store.Domain.Commands.Interfaces;
using Store.Domain.Handlers;
using Store.Domain.Repositories;
using Store.Tests.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        private readonly OrderHandler _orderHandler;
 
        public OrderHandlerTests()
        {
            _customerRepository = new FakeCustomerRepository();
            _deliveryFeeRepository = new FakeDeliveryFeeRepository();
            _discountRepository = new FakeDiscountRepository();
            _orderRepository = new FakeOrderRepository();
            _productRepository = new FakeProductRepository();

            // Inicialize o OrderHandler dentro do construtor
            _orderHandler = new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _orderRepository,
                _productRepository
            );
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "123456789101112"; 
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            command.Validate();
            _orderHandler.Handle(command);
            Assert.AreEqual(_orderHandler.IsValid, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cep_invalido_o_pedido_deve_ser_gerado_normalmente()
        {
            var command = new CreateOrderCommand();
            command.Customer = "12345";
            command.ZipCode = "1224";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
                       
            if (command.ZipCode.Length != 8)
                    command.ZipCode = "00000000";

            _orderHandler.Handle(command);
            Assert.AreEqual(_orderHandler.IsValid, true);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_promocode_inexistente_o_pedido_deve_ser_gerado_normalmente()
        {
            var command = new CreateOrderCommand();
            command.Customer = "12345";
            command.ZipCode = "12345678";
            command.PromoCode = "";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 2));

            command.Validate();
            _orderHandler.Handle(command);
            Assert.AreEqual(_orderHandler.IsValid, true);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_pedido_sem_itens_o_mesmo_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "12345";
            command.ZipCode = "12345678";
            command.PromoCode = "";

            command.Validate();
            _orderHandler.Handle(command);
            Assert.IsFalse(_orderHandler.IsValid);
            //Assert.AreEqual(handler.IsValid, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "11111111111111111111111111111111";
            command.ZipCode = "12345678";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            
            command.Validate();
            Assert.AreEqual(command.IsValid, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_valido_o_pedido_deve_ser_gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "12345678";
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            
            command.Validate();
            _orderHandler.Handle(command);
            Assert.AreEqual(_orderHandler.IsValid, true);
        }
    }
}
