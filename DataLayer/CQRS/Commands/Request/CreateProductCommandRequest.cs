using DataLayer.CQRS.Commands.Response;
using MediatR;

namespace DataLayer.CQRS.Commands.Request
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}