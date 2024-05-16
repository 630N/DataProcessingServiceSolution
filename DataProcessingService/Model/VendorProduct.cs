using DataProcessingService.Helpers.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessingService.Model;

public record VendorProduct
{
    [MapTo("ProductId")]
    public string VendorId { get; set; }
    
    [MapTo("Name")]
    public string ProductName { get; set; }
    
    [MapTo("UnitPrice")]
    public decimal Price { get; set; }
    
    [MapTo("CurrencyCode")]
    public string Currency { get; set; }
    
    [MapTo("StockLevel")]
    public int Quantity { get; set; }
    
    [MapTo("DeliveryAddress")]
    public Address ShippingAddress { get; set; } // Nested object
    
    // ... other properties
}
