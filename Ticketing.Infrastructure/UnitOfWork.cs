using Ticketing.Common.Models;
using Ticketing.Application.Common.Contracts;

namespace Ticketing.Infrastructure;

public class UnitOfWork(TicketingDbContext context) : IUnitOfWork
{
    public async Task<OperationResult> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await context.SaveChangesAsync(cancellationToken);
            return OperationResult.Success();
        }
        catch (Exception exception)
        {            
            return OperationResult.Fail(exception.Message);
        }
    }

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }
    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}