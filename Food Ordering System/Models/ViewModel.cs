using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Food_Ordering_System.Models;

public class CompanyNameVM
{
    [StringLength(100)]
    [Required]
    public string ComName { get; set; }
}

public class FoodItemVM
{
    [StringLength(100)]
    [Required(ErrorMessage = "Please enter a Food Name!")]
    public string FoodName { get; set; }

    [Range(1, 1000, ErrorMessage = "Please enter a valid Food Quantity!")]
    public int FoodQty { get; set; }

    [Range(0.01, 10000.00, ErrorMessage = "Please enter a postive Price value!")]
    [Precision(18, 2)] // Ensures two decimal places
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Please insert Food Image!")]
    public IFormFile? FoodPicFile { get; set; }

    public string? FoodPic { get; set; }
    
    // Testing (can be null) 
    public string? ComId { get; set; }
}


//TODO
