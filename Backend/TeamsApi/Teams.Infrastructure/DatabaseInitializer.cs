using Microsoft.EntityFrameworkCore;

namespace Teams.Infrastructure;

public class DatabaseInitializer
{
    private readonly DbContext _dbContext;

    public DatabaseInitializer(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task InitializeAsync(bool recreateDatabase = false)
    {
        if (recreateDatabase)
            await _dbContext.Database.EnsureDeletedAsync();
            
        await _dbContext.Database.EnsureCreatedAsync();
        
#if DEBUG
        await SeedDatabase();
#endif
    }

#if DEBUG
    private async Task SeedDatabase()
    {
        
    }
#endif
    
}