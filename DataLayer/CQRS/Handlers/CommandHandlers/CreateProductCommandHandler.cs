using DataLayer.CQRS.Commands.Request;
using DataLayer.CQRS.Commands.Response;
using DataLayer.Entities;
using MediatR;

namespace DataLayer.CQRS.Handlers.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var currData = await Product.GetList(request.Name);
            if (currData != null && currData.Count > 0 && currData.Any(x => x.Name == request.Name))
                return new() { IsSuccess = false };
            Product product = new()
            {
                Name = request.Name,
                Price = request.Price
            };
            var result = await product.Upsert();
            return new()
            {
                IsSuccess = result,
                ProductID = product.Id
            };
        }
    }
}