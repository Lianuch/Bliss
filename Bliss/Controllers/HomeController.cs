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
            return View(people);  
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
                return BadRequest();

            using (var streamReader = new StreamReader(csvFile.OpenReadStream()))
            {
                var people = new List<Person>();
                while (streamReader.EndOfStream)
                {
                    var line = await streamReader.ReadLineAsync();
                    var val = line.Split(',');

                    var person = new Person()
                    {
                        Name = val[0],
                        DateOfBirth = DateTime.Parse(val[1]),
                        Married = bool.Parse(val[2]),
                        Phone = val[3],
                        Salary = double.Parse(val[4])

                    };
                    people.Add(person);
                }
            _context.People.AddRange(people);
            await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pers = await _context.People.FindAsync(id);
            if (pers == null)
                return NotFound();
            _context.People.Remove(pers);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
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
