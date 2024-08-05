using FeedBackAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FeedBackAdmin.Controllers
{
    public class MessagesController : Controller
    {
        private readonly FeedbacksContext _context;

        public MessagesController(FeedbacksContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            if (_context.Messages == null)
                return Problem("Message list is empty!");
            List<Message> list = await _context.Messages.ToListAsync();
            string response = JsonConvert.SerializeObject(list);
            return Json(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetDetailsById(int id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            string response = JsonConvert.SerializeObject(message);
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> InsertMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                string response = "Message added successfully!";
                return Json(response);
            }
            return Problem("Problem in adding new message!");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Update(message);
                await _context.SaveChangesAsync();
                string response = "Message updated sccessfully!";
                return Json(response);
            }
            return Problem("Problem in message update!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            if (_context.Messages == null)
            {
                return Problem("Message list is empty!");
            }
            var message = await _context.Messages.FindAsync(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
            }
            await _context.SaveChangesAsync();
            string response = "Message deleted successfully!";
            return Json(response);
        }
    }
}