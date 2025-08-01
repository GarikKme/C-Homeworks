using Homework13.Data;
using Homework13.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework13.Controllers;

public class TeacherController : Controller
{
    private readonly ApplicationDbContext _context;

    public TeacherController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        IEnumerable<Teacher> teachers = _context.Teachers;

        return View(teachers);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Teacher teacher)
    {
        if (ModelState.IsValid)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(teacher);
    }
    
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var teacher = _context.Teachers.Find(id);

        if (teacher == null)
        {
            return NotFound();
        }

        return View(teacher);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Teacher teacher)
    {
        if (ModelState.IsValid)
        {
            _context.Teachers.Update(teacher);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(teacher);
    }
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var teacher = _context.Teachers.Find(id);

        if (teacher == null)
        {
            return NotFound();
        }

        return View(teacher);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteTeacher(int? id)
    {
        var teacher = _context.Teachers.Find(id);
        if (teacher == null)
        {
            return NotFound();
        }
        _context.Teachers.Remove(teacher);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
