namespace StarWarsFleet.Application.Shared.UseCases.Abstractions;

public interface IHandler<in TRequest, TResponse> where TRequest : IRequest where TResponse : IResponse
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}