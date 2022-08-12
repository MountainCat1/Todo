using Authentication.Domain.Abstractions;

namespace Authentication.Domain.Entities;

public class Account : IEntity
{
    public Guid Guid { get; set; }
    public Guid UserGuid { get; set; }
    public string PasswordHash { get; set; }
}