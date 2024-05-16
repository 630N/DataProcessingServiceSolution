# DataProcessingLibrary

This library provides a generic solution for validating, and transforming data from any custom input data to a standardized format (defined by user as well) for further processing.

***
    This Library is not production ready and is only for learning and practices!
***

## Features

- Robust validation system with custom error handling
- Flexible data transformation with support for nested objects
- Attribute-based property mapping for easy configuration

## Getting Started

### Installation

To use this library in your project, install it via NuGet or add the DLL reference directly.

### Usage

Here's a quick example of how to use the library:

```csharp
// Configure your services
var validator = new Validator<VendorProduct>();
var transformer = new Transformer<VendorProduct, StandardProduct>();
var service = new Service<VendorProduct, StandardProduct>(validator, transformer);

try
{
    var inputData = new VendorProduct(){ /* ... */ };
    var result = service.Transform(vendorProduct);
    // Use the result as needed
}
catch (TransformationException ex)
{
    // Handle the exception, log it, or display an error message
}
