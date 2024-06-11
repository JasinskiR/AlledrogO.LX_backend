using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
using AlledrogO.User.Core.EF.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.E2ETests.Modules.UserModule;

public abstract class BaseIntegrationTest
    : IClassFixture<WebAppFactory>,
        IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly ICommandDispatcher CommandDispatcher;
    protected readonly IQueryDispatcher QueryDispatcher;
    protected readonly UserDbContext DbContext;

    protected BaseIntegrationTest(WebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        CommandDispatcher = _scope.ServiceProvider.GetRequiredService<ICommandDispatcher>();
        QueryDispatcher = _scope.ServiceProvider.GetRequiredService<IQueryDispatcher>();

        DbContext = _scope.ServiceProvider
            .GetRequiredService<UserDbContext>();
    }

    public void Dispose()
    {
        _scope?.Dispose();
        DbContext?.Dispose();
    }
}