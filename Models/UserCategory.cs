using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRestaurant.Models;

public partial class UserCategory
{
    public decimal Id { get; set; }

    public string? CategoryName { get; set; }

    public string? ImagePath { get; set; }
    
    [NotMapped]
    public virtual IFormFile ImgFile { get; set; }

    public virtual ICollection<UserProduct> UserProducts { get; set; } = new List<UserProduct>();
}
