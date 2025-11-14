using Food_Ordering_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Food_Ordering_System.Controllers
{
    public class SellerController : Controller
    {

        private readonly DB _context;

        public SellerController(DB context)
        {
            _context = context;
        }

        public string NextId()
        {
            var last = _context.FoodItems
                .OrderByDescending(foods => foods.FoodId)
                .FirstOrDefault();

            if (last == null || string.IsNullOrEmpty(last.FoodId))
            {
                return "FI001";
            }

            string lastId = last.FoodId.Substring(1);

            if (int.TryParse(lastId, out int numericPart))
            {
                numericPart++;
                return "FI" + numericPart.ToString("D5");
            }

            return "FI00001";
        }

        [HttpGet]
        //[Authorize(Roles = "Seller")] 
        public IActionResult AddFood()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Seller")]
        public async Task<IActionResult> AddFood(FoodItemVM food)
        {
            if (food.FoodPicFile == null)
            {
                ModelState.AddModelError("FoodPicFile", "Please Upload a Food Image!");
            }

            if (await _context.FoodItems.AnyAsync(foods => foods.FoodName.Trim().ToLower() == food.FoodName.Trim().ToLower()))
            {
                ModelState.AddModelError("FoodName", "This Food already exists!");
            }

            if (food.FoodQty < 0)
            { 
                ModelState.AddModelError("FoodQty", "Quantity cannot be lesser than 0!");
            }

            if (!ModelState.IsValid)
            {
                return View(food);
            }

            string FoodPic = "";

            if (food.FoodPicFile != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image/Food");
                
                if (!Directory.Exists(uploadsFolder)) { 
                    Directory.CreateDirectory(uploadsFolder);
                }

                FoodPic = Guid.NewGuid().ToString() + Path.GetExtension(food.FoodPicFile.FileName);
                var savePath = Path.Combine(uploadsFolder, FoodPic);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await food.FoodPicFile.CopyToAsync(stream);
                }
            }

            var newFood = new FoodItem
            {
                FoodId = NextId(),
                FoodName = food.FoodName,
                FoodQty = food.FoodQty,
                Price = food.Price,
                FoodPic = FoodPic,
                ComId = null // Testing (set to null until company is added)
            };
            _context.FoodItems.Add(newFood);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");

            
        }
    }

}
