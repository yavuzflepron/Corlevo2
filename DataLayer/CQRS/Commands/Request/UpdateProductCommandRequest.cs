using DataLayer.CQRS.Commands.Response;
using MediatR;

namespace DataLayer.CQRS.Commands.Request
{
    public class UpdateProductCommandRequest : IRequest<UpdateProductCommandResponse>
    {
        public Guid ProductID { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
    }
}