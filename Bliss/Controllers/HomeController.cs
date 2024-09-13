using Bliss.Models;
using Bliss.Data; 
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Task.Models;

namespace Bliss.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        public HomeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var people = await _context.People.ToListAsync();
            return View(people);        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
                return Json(new{success = false});

            using (var reader = new StreamReader(csvFile.OpenReadStream()))
            {
                var line = string.Empty;
                var people = new List<Person>();

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var values = line.Split(',');
                    if (values.Length == 5)
                    {
                        var person = new Person
                        {
                            Name = values[0],
                            DateOfBirth = DateTime.Parse(values[1]),
                            Married = bool.Parse(values[2]),
                            Phone = values[3],
                            Salary = double.Parse(values[4])
                        };
                        people.Add(person);
                    }
                }

                _context.People.AddRange(people);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Update(person);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
        [HttpDelete("Home/Delete{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pers = await _context.People.FindAsync(id);
            if (pers == null)
                return NotFound();
            _context.People.Remove(pers);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
