using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentCheckApp.Data;
using StudentCheckApp.Models.DbModel;

namespace StudentCheckApp.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Teachers> _userManager;

        public TeachersController(ApplicationDbContext context, UserManager<Teachers> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            var teacher = await _userManager.Users.ToListAsync();
            return View(teacher);
        }
    }
}
