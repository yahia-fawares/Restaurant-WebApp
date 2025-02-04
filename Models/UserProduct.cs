using System;
using System.Collections.Generic;

namespace MyRestaurant.Models;

public partial class UserProduct
{
    public decimal Id { get; set; }

    public string? Name { get; set; }

    public decimal? Sale { get; set; }

    public decimal? Price { get; set; }

    public decimal? CategoryId { get; set; }

    public virtual UserCategory? Category { get; set; }

    public virtual ICollection<UserProductCustomer> UserProductCustomers { get; set; } = new List<UserProductCustomer>();
}
