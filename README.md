## Design Approach
**Entity Framework** - I have used the for better performance and maintenance of the code. Along with EF I have used the repository design for abstraction and centralize the model.

**  

## Depdendency injection 
I have injected the DB context in container to keep it abstract and done the unit testing with mock database. 

## Logging
Project is using the NLog framework for logging the errors and information

## Security
I have implemented the bearer token to restrict and manage the access levels for this API. 
For development I have by-pass the Authorization and token requirement in Debug mode. 
To test the security, Run the project in Release mode and test the project in Postman.  It would return the access denied error if no token has been provided. To generate the testing token, Call the API/GetToken method and pass in all the end points in header (bearer token) to get access.


## Documentation and Schema
Swagger has been implemented for API documentation and end points testing. 

## Unit Testing Project
Visual Studio testing framework has been used for testing the project controller(s) and have mocked the DBcontext to remove the dependency.  

## Instructions

To set up the project:

* Open project in VS.
* Restore nuget packages and rebuild.
* Run the project.

There should be these endpoints:

1. `GET /products` - gets all products.
2. `GET /products?name={name}` - finds all products matching the specified name.
3. `GET /products/{id}` - gets the project that matches the specified ID - ID is a GUID.
4. `POST /products` - creates a new product.
5. `PUT /products/{id}` - updates a product.
6. `DELETE /products/{id}` - deletes a product and its options.
7. `GET /products/{id}/options` - finds all options for a specified product.
8. `GET /products/{id}/options/{optionId}` - finds the specified product option for the specified product.
9. `POST /products/{id}/options` - adds a new product option to the specified product.
10. `PUT /products/{id}/options/{optionId}` - updates the specified product option.
11. `DELETE /products/{id}/options/{optionId}` - deletes the specified product option.

All models are specified in the `/Models` folder, but should conform to:

**Product:**
```
{
  "Id": "01234567-89ab-cdef-0123-456789abcdef",
  "Name": "Product name",
  "Description": "Product description",
  "Price": 123.45,
  "DeliveryPrice": 12.34
}
```

**Products:**
```
{
  "Items": [
    {
      // product
    },
    {
      // product
    }
  ]
}
```

**Product Option:**
```
{
  "Id": "01234567-89ab-cdef-0123-456789abcdef",
  "Name": "Product name",
  "Description": "Product description"
}
```

**Product Options:**
```
{
  "Items": [
    {
      // product option
    },
    {
      // product option
    }
  ]
}
```
