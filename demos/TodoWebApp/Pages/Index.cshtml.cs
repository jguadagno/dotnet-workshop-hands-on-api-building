using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoWebApp.Models;
using TodoWebApp.Services;

namespace TodoWebApp.Pages;

public sealed class IndexModel(TodoStore todoStore) : PageModel
{
    public IReadOnlyList<TodoItem> Items { get; private set; } = [];

    [BindProperty]
    [Required]
    [StringLength(80)]
    public string NewItemTitle { get; set; } = string.Empty;

    public void OnGet()
    {
        Items = todoStore.GetAll();
    }

    public IActionResult OnPostAdd()
    {
        if (!ModelState.IsValid)
        {
            Items = todoStore.GetAll();
            return Page();
        }

        todoStore.Add(NewItemTitle);
        return RedirectToPage();
    }

    public IActionResult OnPostToggle(int id)
    {
        todoStore.Toggle(id);
        return RedirectToPage();
    }

    public IActionResult OnPostDelete(int id)
    {
        todoStore.Delete(id);
        return RedirectToPage();
    }
}
