using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoBackend.Models;
using TodoBackend.ViewModels;

namespace TodoBackend.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _repo;

        public TodoController(ITodoRepository repo)
        {
            _repo = repo;
        }

        [Route("getalltodo")]
        [HttpGet]
        public IActionResult Home()
        {
            var allTodoItems = _repo.GetAll();
            var views = allTodoItems
                .Select(r => new TodoItemView(r, Request));
            return Ok(views);
        }

        [Route("posttodo")]
        [HttpPost]
        public IActionResult NewTodo(TodoItem item)
        {
            item.Id = Guid.NewGuid().ToString();
            item.Completed = false;

            _repo.AddNew(item);

            return Ok(new TodoItemView(item, Request));
        }

        [Route("gettodo/{id}")]
        [HttpGet]
        public IActionResult GetTodo(string id)
        {
            var item = _repo.Get(id);
            return Ok(new TodoItemView(item, Request));
        }

        [Route("deletetodo/{id?}")]
        [HttpDelete]
        public IActionResult DeleteTodo(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                _repo.Delete(id);
                return Ok();
            }

            _repo.DeleteAll();
            return Ok();
        }

        [Route("edittodo/{id}")]
        [HttpPatch]
        public IActionResult ChangeTodo(string id, TodoItem todo)
        {
            todo.Id = id;

            _repo.Update(todo);

            var item = _repo.Get(todo.Id);

            return Ok(new TodoItemView(item, Request));
        }
    }
}
