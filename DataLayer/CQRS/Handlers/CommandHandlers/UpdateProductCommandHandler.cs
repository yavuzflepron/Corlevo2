using DataLayer.CQRS.Commands.Request;
using DataLayer.CQRS.Commands.Response;
using DataLayer.Entities;
using MediatR;

namespace DataLayer.CQRS.Handlers.CommandHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await Product.GetById(request.ProductID);
            if (product == null) return new() { IsSuccess = false };
            if (!string.IsNullOrEmpty(request.Name))
            {
                var currData = await Product.GetList(request.Name);
                if (currData != null && currData.Count > 0 && currData.Any(x => x.Id != request.ProductID && x.Name == request.Name))
                    return new() { IsSuccess = false };
                product.Name = request.Name;
            }
            if (request.Price.HasValue) product.Price = request.Price.Value;
            var result = await product.Upsert();
            return new()
            {
                IsSuccess = result
            };
        }
    }
}