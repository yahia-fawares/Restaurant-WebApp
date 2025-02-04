using System;
using System.Collections.Generic;

namespace MyRestaurant.Models;

public partial class UserLogin
{
    public decimal Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public decimal? RoleId { get; set; }

    public decimal? CustomerId { get; set; }

    public virtual UserCustomer? Customer { get; set; }

    public virtual UserRole? Role { get; set; }
}
