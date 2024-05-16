namespace DataProcessingService.Model;

public record StandardProduct
{
    public string ProductId { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public string CurrencyCode { get; set; }
    public int StockLevel { get; set; }
    public Address DeliveryAddress { get; set; } // Nested object
    
    // ... other properties
}
