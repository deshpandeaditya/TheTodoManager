using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheTodoManager.BLL.Interfaces;
using TheTodoManager.Models;

namespace TheTodoManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private ITodoService todoService;
        public TodoController(ITodoService _todoService)
        {
            this.todoService = _todoService;
        }
        // GET: api/<TodoController>
        [HttpGet]
        public IEnumerable<Todo> Get()
        {
            return todoService.GetItems();
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public Todo Get(int id)
        {
            return todoService.Get(id);
        }
    }
}
