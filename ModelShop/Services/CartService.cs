using ModelShop.Data.Contracts;
using ModelShop.Models;

namespace ModelShop.Services
{
    public class CartService : ICartService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IModel3DRepository _model3DRepository;
        private readonly ICart_Model3DRepository _cart_Model3DRepository;
        private readonly IOrder_Model3DRepository _order_Model3DRepository;


        public CartService(IClientRepository clientRepository,
            ICartRepository cartRepository, 
            IOrder_Model3DRepository client_Model3DRepository,
            IOrderRepository orderRepository,
            IModel3DRepository model3DRepository,
            ICart_Model3DRepository cart_Model3DRepository)
        {
            _clientRepository = clientRepository;
            _cartRepository = cartRepository;
            _order_Model3DRepository = client_Model3DRepository;
            _orderRepository = orderRepository;
            _model3DRepository = model3DRepository;
            _cart_Model3DRepository = cart_Model3DRepository;
        }

        public void AddToCart(string clientId, int modelId)
        {
            // check is modelId present in cart or not
            if(IsInCart(clientId, modelId))
            {
                throw new InvalidOperationException("Model is already in the cart");
            }

            var cart = _cartRepository.GetCartByClientId(clientId);

            // add 
            _cart_Model3DRepository.Insert
                (new Cart_Model3D { CartID = cart.CartID, Model3DID = modelId });

            _cart_Model3DRepository.Save();
        }

        public Cart GetCart(string clientId)
        {
            return _cartRepository.GetCartByClientId(clientId);
        }

        public bool IsInCart(string clientId, int modelId)
        {
            // replace with exist method
            var model3D = _model3DRepository.Get(modelId);

            if (model3D == null)
                throw new ArgumentException("There not such model with given id");

            return _cartRepository.GetCartByClientId(clientId)
                .Cart_Models3D.Any(c_m => c_m.Model3DID == modelId);
        }

        public bool IsOrdered(string clientId, int modelId)
        {
            return _clientRepository.IsModel3DOrdered(clientId, modelId);
        }

        public void RemoveFromCart(string clientId, int modelId)
        {
            if(IsInCart(clientId, modelId) == false)
            {
                throw new InvalidOperationException("Model not in cart");
            }

            // find exact mapping between Cart and Model3D and remove them
            var cart_Model3D = _cartRepository.GetCartByClientId(clientId)
                .Cart_Models3D.FirstOrDefault(c_m => c_m.Model3DID == modelId);

            // delete
            _cart_Model3DRepository.Delete(cart_Model3D);

            _cart_Model3DRepository.Save();
        }
    }
}
