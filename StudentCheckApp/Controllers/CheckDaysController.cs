using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentCheckApp.Data;
using StudentCheckApp.Models.DbModel;

namespace StudentCheckApp.Controllers
{
    public class CheckDaysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckDaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CheckDays
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            
            List<CheckDay> checkDays = new List<CheckDay>();
            foreach (var item in students)
            {
                var dbCheckDays = await _context.CheckDay.Where(x=>x.Students.ID == item.ID).ToListAsync();
                foreach (var item2 in dbCheckDays)
                {
                    item2.Students = item;
                    checkDays.Add(item2);
                }
                

            }
            
            return View(checkDays);
        }

        // GET: CheckDays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkDay = await _context.CheckDay
                .FirstOrDefaultAsync(m => m.ID == id);
            if (checkDay == null)
            {
                return NotFound();
            }

            return View(checkDay);
        }

        // GET: CheckDays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CheckDays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Status")] CheckDay checkDay,int studentId)
        {
            if (ModelState.IsValid)
            {
                var student = _context.Students.Find(studentId);
                checkDay.Students = student;
                checkDay.Date = DateTime.Now;
                _context.Add(checkDay);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Details", "Students", new { id = studentId });
            }
            return View(checkDay);
        }

        // GET: CheckDays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkDay = await _context.CheckDay.FindAsync(id);
            if (checkDay == null)
            {
                return NotFound();
            }
            return View(checkDay);
        }

        // POST: CheckDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date,Status")] CheckDay checkDay)
        {
            if (id != checkDay.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckDayExists(checkDay.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(checkDay);
        }

        // GET: CheckDays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkDay = await _context.CheckDay
                .FirstOrDefaultAsync(m => m.ID == id);
            if (checkDay == null)
            {
                return NotFound();
            }

            return View(checkDay);
        }

        // POST: CheckDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checkDay = await _context.CheckDay.FindAsync(id);
            _context.CheckDay.Remove(checkDay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckDayExists(int id)
        {
            return _context.CheckDay.Any(e => e.ID == id);
        }
    }
}
