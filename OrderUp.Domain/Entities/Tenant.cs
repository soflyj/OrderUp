using System;
using System.Collections.Generic;

namespace OrderUp.Domain.Entities;

public class Tenant
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;

    public ICollection<User> Users { get; set; } = new List<User>();
}
