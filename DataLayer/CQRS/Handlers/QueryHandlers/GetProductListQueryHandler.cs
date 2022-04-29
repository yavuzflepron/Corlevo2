using DataLayer.CQRS.Queries.Request;
using DataLayer.CQRS.Queries.Response;
using DataLayer.Entities;
using MediatR;

namespace DataLayer.CQRS.Handlers.QueryHandlers
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQueryRequest, List<GetProductListQueryResponse>>
    {
        public async Task<List<GetProductListQueryResponse>> Handle(GetProductListQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await Product.GetList(request.Search);
            return data != null && data.Count > 0 ? data.Select(x => new GetProductListQueryResponse() { ProductID = x.Id, Name = x.Name, Price = x.Price }).ToList() : null;
        }
    }
}