using Microsoft.EntityFrameworkCore;

namespace TodoApi.Data;

public class DatabaseInitializer
{
    private DbContext _dbContext;

    public DatabaseInitializer(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task InitializeAsync()
    {
        await _dbContext.Database.EnsureCreatedAsync();
        
#if DEBUG
        await SeedDatabase();
#endif
    }

#if DEBUG
    private async Task SeedDatabase()
    {
        // TODO: add some seeding for database
    }
#endif
    
}