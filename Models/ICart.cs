namespace GentsOutfits_Authentication_.Models
{
    public interface ICart
    {
        public void Add(CartItems cartItem);
        public void Update(CartItems cartItem);
        public void Remove(CartItems cartItem);
        public List<CartItems> GetItemsByUser(string userId);
        public CartItems GetItem(int productId, string userId);




    }
}
