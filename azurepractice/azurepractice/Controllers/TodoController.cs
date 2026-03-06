using azurepractice.Dto.Request;
using azurepractice.Entity;
using Microsoft.AspNetCore.Mvc;

namespace azurepractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly List<Todo> _todos = new();
        private static int _nextId = 1;

        [HttpGet]
        public IActionResult GetAll() 
            => Ok(_todos);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo is null)
                return NotFound();

            return Ok(todo);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] CreateTodoRequest request)
        {
            var todo = new Todo(_nextId++, request.Title, false);
            _todos.Add(todo);
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateTodoRequest request)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo is null) return NotFound();

            todo.Title = request.Title;
            todo.IsDone = request.IsDone;
            return Ok(todo);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo is null) return NotFound();

            _todos.Remove(todo);
            return NoContent();
        }
    }
}
