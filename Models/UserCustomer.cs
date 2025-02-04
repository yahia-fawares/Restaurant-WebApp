using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRestaurant.Models;

public partial class UserCustomer
{
    public decimal Id { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public string? ImagePath { get; set; }

    [NotMapped]
    public virtual IFormFile ImgFile { get; set; }

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();

    public virtual ICollection<UserProductCustomer> UserProductCustomers { get; set; } = new List<UserProductCustomer>();
}
