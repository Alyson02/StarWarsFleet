using StarWarsFleet.Domain.Models;

namespace StarWarsFleet.Application.Shared.UseCases.Abstractions;

public interface IHandler<in TRequest, TResponse>
    where TRequest : IRequest
    where TResponse : class
{
    Task<ResponseModel<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}