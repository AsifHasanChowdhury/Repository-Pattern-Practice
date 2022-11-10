namespace RepositoryPatternPractice.Models.Data_Access_Layer
{
    public interface IUsersRepository
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
      

    }
}
