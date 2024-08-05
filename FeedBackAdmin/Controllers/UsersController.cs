using FeedBackAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FeedBackAdmin.Controllers
{
    public class UsersController : Controller
    {
        private readonly FeedbacksContext _context;

        public UsersController(FeedbacksContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            if (_context.Users == null)
                return Problem("User list is empty!");
            List<User> list = await _context.Users.ToListAsync();
            string response = JsonConvert.SerializeObject(list);
            return Json(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetDetailsById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            string response = JsonConvert.SerializeObject(user);
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> InsertUser(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                string response = "User added successfully!";
                return Json(response);
            }
            return Problem("Problem in adding new user!");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                string response = "User updated sccessfully!";
                return Json(response);
            }
            return Problem("Problem in user update!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return Problem("User list is empty!");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            await _context.SaveChangesAsync();
            string response = "User deleted successfully!";
            return Json(response);
        }
    }
}