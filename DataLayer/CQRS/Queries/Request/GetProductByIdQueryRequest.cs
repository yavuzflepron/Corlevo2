using DataLayer.CQRS.Queries.Response;
using MediatR;

namespace DataLayer.CQRS.Queries.Request
{
    public class GetProductByIdQueryRequest : IRequest<GetProductByIdQueryResponse>
    {
        public Guid ProductID { get; set; }
    }
}