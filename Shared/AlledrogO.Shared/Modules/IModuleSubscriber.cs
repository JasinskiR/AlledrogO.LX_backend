namespace AlledrogO.Shared.Modules;

public interface IModuleSubscriber
{
    IModuleSubscriber Subscriber<TRequest, TResponse>(string path, 
        Func<TRequest, IServiceProvider, CancellationToken, Task<TResponse>> action)
    where TRequest : class
    where TResponse : class;
}