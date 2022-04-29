using DataLayer.CQRS.Queries.Response;
using MediatR;

namespace DataLayer.CQRS.Queries.Request
{
    public class GetProductListQueryRequest : IRequest<List<GetProductListQueryResponse>>
    {
        public string? Search { get; set; }
    }
}