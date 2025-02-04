using System;
using System.Collections.Generic;

namespace MyRestaurant.Models;

public partial class UserProductCustomer
{
    public decimal Id { get; set; }

    public decimal? ProductId { get; set; }

    public decimal? CustomerId { get; set; }

    public decimal? Quantity { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public virtual UserCustomer? Customer { get; set; }

    public virtual UserProduct? Product { get; set; }
}
