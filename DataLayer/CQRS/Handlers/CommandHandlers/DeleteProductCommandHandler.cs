using DataLayer.CQRS.Commands.Request;
using DataLayer.CQRS.Commands.Response;
using DataLayer.Entities;
using MediatR;

namespace DataLayer.CQRS.Handlers.CommandHandlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await Tools.DataTools.Delete<Product>(request.ProductID);
            return new()
            {
                IsSuccess = result
            };
        }
    }
}