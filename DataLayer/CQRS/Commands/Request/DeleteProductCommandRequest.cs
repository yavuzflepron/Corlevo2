using DataLayer.CQRS.Commands.Response;
using MediatR;

namespace DataLayer.CQRS.Commands.Request
{
    public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse>
    {
        public Guid ProductID { get; set; }
    }
}