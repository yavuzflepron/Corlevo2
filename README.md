# Corlevo2
Demo CRUD API for Corlevo using CQRS pattern. Developed for basic CRUD operations.

Used Technologies:

* CQRS
* MediatR
* .Net Core 6.0
* Entity Framework
* Google Cloud Datastore (Demo Account - NoSql)
* Open API (Swagger)

### How To Run:
The application can be run with the `dotnet run` command.

The API will be served on https://localhost:7299

The API document URL: https://localhost:7299/swagger/index.html


### Endpoints:
* **/Products [GET]:** For seeking products with `search` parameter.
* **/Products/{Product ID} [GET]:** For get a product with the ID value.
* **/Products [POST]:** For add new product
* **/Products/{Product ID} [PUT]:** For update an existing product
* **/Products/{Product ID} [DELETE]:** For delete a product with the ID value.
