using DataLayer.CQRS.Queries.Request;
using DataLayer.CQRS.Queries.Response;
using DataLayer.Entities;
using MediatR;

namespace DataLayer.CQRS.Handlers.QueryHandlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>
    {
        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await Product.GetById(request.ProductID);
            if (product == null) throw new KeyNotFoundException("Invalid Product ID");
            return new()
            {
                ProductID = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}