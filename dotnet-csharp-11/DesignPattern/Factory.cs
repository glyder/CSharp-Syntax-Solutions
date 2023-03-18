public class Factory
{
    public IProduct CreateProduct(string productName)
    {
        return productName switch
        {
            "Product1" => new Product(),
            "Product2" => new Product2(),
            _ => throw new ArgumentException("Invalid product name"),
        };
    }
}
