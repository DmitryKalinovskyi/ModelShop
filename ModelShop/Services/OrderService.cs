using ModelShop.Data.Contracts;
using ModelShop.Models;

namespace ModelShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICart_Model3DRepository _cart_Model3DRepository;
        private readonly IOrder_Model3DRepository _order_Model3DRepository;

        public OrderService(ICartRepository cartRepository,
            IClientRepository clientRepository,
            IOrderRepository orderRepository,
            ICart_Model3DRepository cart_Model3DRepository,
            IOrder_Model3DRepository order_Model3DRepository) 
        {
            _cartRepository = cartRepository;
            _clientRepository = clientRepository;
            _orderRepository = orderRepository;
            _cart_Model3DRepository = cart_Model3DRepository;
            _order_Model3DRepository = order_Model3DRepository;
        }

        public Order CompleteOrder(string clientId)
        {
            // to do this we need first create order, move all models to Client_Models and clear Cart

            // create new order 


            var cart = _cartRepository.GetCartByClientId(clientId);

            if (cart.Cart_Models3D.Count() == 0)
                throw new InvalidOperationException("You need to select at least one model");

            var order = new Order
            {
                ClientID = clientId,
                CreatedDate = DateTime.Now,
            };
            _orderRepository.Insert(order);

            // assing to client new models
            foreach (var item in cart.Cart_Models3D)
            {
                _order_Model3DRepository.Insert(new Order_Model3D
                {
                    Order = order,
                    Model3DID = item.Model3DID
                });

                _cart_Model3DRepository.Delete(item);
            }

            _orderRepository.Save();

            return order;
        }

        public IEnumerable<Model3D> GetOrderedModels3D(string clientId)
        {
            return _clientRepository.GetOrderedModels(clientId);
        }
    }
}
