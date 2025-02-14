namespace GentsOutfits_Authentication_.Models
{
    public interface IProduct
    {
        public void Add(Product product);
        public Product GetById(int ID);
        public Product Edit(int ID);
        public void Update(Product product);
        public void Delete(int ID);
        public List<Product> get(string categry);
        public IEnumerable<Product> GetAll();
        public Product GetItem(int ID);
        public int GetStockQuantity(int productId);
        public void UpdateStockQuantity(int productId, int newQuantity);



    }
}
