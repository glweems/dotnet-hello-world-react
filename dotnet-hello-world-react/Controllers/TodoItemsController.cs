using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApi.Models;

namespace TodoItems.Controllers {
  #region TodoController
  [Route ("api/[controller]")]
  [ApiController]
  public class TodoItemsController : ControllerBase {
    private readonly TodoContext _context;

    public TodoItemsController (TodoContext context) {
      _context = context;
    }
    #endregion

    #region snippet_GetByID
    // GET: api/TodoItems/5
    [HttpGet ("{id}")]
    public async Task<ActionResult<TodoItem>> GetTodoItem (long id) {
      var todoItem = await _context.TodoItems.FindAsync (id);

      if (todoItem == null) {
        return NotFound ();
      }

      return todoItem;
    }
    #endregion

    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems () {
      return await _context.TodoItems.ToListAsync ();
    }

    // POST: api/TodoItems
    [HttpPost]
    public async Task<ActionResult<TodoItem[]>> PostTodoItem (TodoItem todoItem) {
      _context.TodoItems.Add (todoItem);
      await _context.SaveChangesAsync ();

      //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
      // return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
      return _context.TodoItems.ToArray ();
    }

    // PUT: api/TodoItems/5
    [HttpPut ("{id}")]
    public async Task<IActionResult> PutTodoItem (long id, TodoItem todoItem) {
      if (id != todoItem.Id) {
        return BadRequest ();
      }

      _context.Entry (todoItem).State = EntityState.Modified;

      try {
        await _context.SaveChangesAsync ();
      } catch (DbUpdateConcurrencyException) {
        if (!TodoItemExists (id)) {
          return NotFound ();
        } else {
          throw;
        }
      }

      return NoContent ();
    }
    private bool TodoItemExists (long id) {
      return _context.TodoItems.Any (e => e.Id == id);
    }
  }
}