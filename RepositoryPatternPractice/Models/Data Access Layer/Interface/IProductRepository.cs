namespace RepositoryPatternPractice.Models.Data_Access_Layer
{
    public interface IProductRepository
    {

        //CREATE
        void InsertProduct(Product_Table product_);

        //READ
        List<Product_Table> GetProducts();

        Product_Table GetProductById(int ProductId);
        
        //UPDATE
        void UpdateProduct(Product_Table product_);

        //DELETE
        void DeleteProduct(int ProductId);


        List <Product_Table> GetProductHistorybyId(int ProductId);

        public List<string> GetFormField();




    }
}
