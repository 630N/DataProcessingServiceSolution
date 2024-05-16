using DataProcessingService.Model;
using DataProcessingService.Transformation;
using DataProcessingService.Validation;
using System.Net;

namespace DataProcessingService.Test;

public class DataProcessingServiceTests
{
    [Fact]
    public void Transform_ValidVendorProduct_ReturnsStandardProductWithCorrectMappings()
    {
        // Arrange
        var transformer = new Transformer<VendorProduct, StandardProduct>();
        var vendorProduct = new VendorProduct
        {
            VendorId = "VP123",
            ProductName = "Gaming Mouse",
            ShippingAddress = new Address
            {
                Street = "123 Tech Lane",
                City = "Innovation City"
            }
        };

        // Act
        var standardProduct = transformer.Transform(vendorProduct);

        // Assert
        Assert.Equal(vendorProduct.VendorId, standardProduct.ProductId);
        Assert.Equal(vendorProduct.ProductName, standardProduct.Name);
        Assert.Equal(vendorProduct.ShippingAddress.Street, standardProduct.DeliveryAddress.Street);
        Assert.Equal(vendorProduct.ShippingAddress.City, standardProduct.DeliveryAddress.City);
    }

    [Fact]
    public void Validate_VendorProductWithMissingFields_ReturnsValidationResults()
    {
        // Arrange
        var validator = new Validator<VendorProduct>();
        var vendorProduct = new VendorProduct
        {
            // VendorId is missing
            ProductName = "Gaming Mouse",
            ShippingAddress = new Address
            {
                // Street is missing
                City = "Innovation City"
            }
        };

        // Act
        var validationResults = validator.Validate(vendorProduct);

        // Assert
        Assert.False(validationResults.IsValid);
        Assert.Equal(validationResults.Errors.Count, 6);
        Assert.True(validationResults.Errors.Any(v => v.Contains("VendorId")));
        Assert.True(validationResults.Errors.Any(v => v.Contains("ShippingAddress")));
    }

    [Fact]
    public void ProcessData_WithInvalidInput_ReturnsValidationErrorsAndNoOutput()
    {
        // Arrange
        var transformer = new Transformer<VendorProduct, StandardProduct>();
        var validator = new Validator<VendorProduct>();
        var service = new Service<VendorProduct, StandardProduct>(validator, transformer);
        var vendorProduct = new VendorProduct
        {
            // VendorId is missing
            ProductName = "Gaming Mouse",
            ShippingAddress = new Address
            {
                // Street is missing
                City = "Innovation City"
            }
        };

        // Act
        var standardProduct = service.Transform(vendorProduct);

        // Assert
        Assert.Equal(standardProduct, default(StandardProduct));
    }
}