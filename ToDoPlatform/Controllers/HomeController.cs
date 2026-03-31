// Controllers/HomeController.cs
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoPlatform.Data;
using ToDoPlatform.Models;
using ToDoPlatform.Services;
using ToDoPlatform.ViewModels;
namespace ToDoPlatform.Controllers;
[Authorize]

public class HomeController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly IUserService _userService;
    public HomeController(AppDbContext dbContext, IUserService
userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }  

    public async Task<IActionResult> Index()
    {
        var user = await _userService.GetLoggedUser();
        var openTodos = await _dbContext.ToDos
            .Where(t => t.UserId == user.Id && !t.Done)
            .OrderByDescending(t => t.CreatedAt)
            .ThenBy(t => t.Title)
            .ToListAsync();
        var totalTasks = await _dbContext.ToDos.CountAsync(t => t.UserId
== user.Id);
        var endedTasks = await _dbContext.ToDos.CountAsync(t => t.UserId
== user.Id && t.Done);

        HomeVM homeVM = new()
        {  
            TotalTasks = totalTasks,
            OpenTasks = openTodos.Count,
            EndedTasks = endedTasks,
            ToDos = openTodos
        };

        return View(homeVM);
    }

    [HttpPost]
    public async Task<IActionResult> EndTask(int? id)
    {
        if (id == null)
        {
            return Json(new
            {
                success = false,
                message = "Problemas ao carregar a tarefa! Tente novamente mais tarde..."
            });
        }
        var todo = await _dbContext.ToDos.Where(t => t.Id ==
id).SingleOrDefaultAsync();
        if (todo == null)
        {
            return Json(new
            {          
                success = false,
                message = "Não foi possível localizar a tarefa! Tente novamente mais tarde..."
            });
        }
        var user = await _userService.GetLoggedUser();
        if (user == null)
        {
            return Json(new
            {
                success = false,