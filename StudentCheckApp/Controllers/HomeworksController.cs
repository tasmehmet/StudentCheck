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
    public class HomeworksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeworksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Homeworks
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();

            List<Homeworks> homeworks = new List<Homeworks>();
            foreach (var item in students)
            {
                var dbHomeworks = await _context.Homeworks.Where(x => x.Students.ID == item.ID).ToListAsync();
                foreach (var item2 in dbHomeworks)
                {
                    item2.Students = item;
                    homeworks.Add(item2);
                }


            }
            return View(homeworks);
        }

        // GET: Homeworks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeworks = await _context.Homeworks
                .FirstOrDefaultAsync(m => m.ID == id);
            if (homeworks == null)
            {
                return NotFound();
            }

            return View(homeworks);
        }

        // GET: Homeworks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Homeworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Note,Status")] Homeworks homeworks, int studentId)
        {
            if (ModelState.IsValid)
            {
                var student = _context.Students.Find(studentId);
                homeworks.Students = student;
                homeworks.CreateTime = DateTime.Now;
                _context.Add(homeworks);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Students", new { id = studentId });
            }
            return View(homeworks);
        }

        // GET: Homeworks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeworks = await _context.Homeworks.FindAsync(id);
            if (homeworks == null)
            {
                return NotFound();
            }
            return View(homeworks);
        }

        // POST: Homeworks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Note,Status")] Homeworks homeworks)
        {
            if (id != homeworks.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homeworks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeworksExists(homeworks.ID))
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
            return View(homeworks);
        }

        // GET: Homeworks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeworks = await _context.Homeworks
                .FirstOrDefaultAsync(m => m.ID == id);
            if (homeworks == null)
            {
                return NotFound();
            }

            return View(homeworks);
        }

        // POST: Homeworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeworks = await _context.Homeworks.FindAsync(id);
            _context.Homeworks.Remove(homeworks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeworksExists(int id)
        {
            return _context.Homeworks.Any(e => e.ID == id);
        }
    }
}
