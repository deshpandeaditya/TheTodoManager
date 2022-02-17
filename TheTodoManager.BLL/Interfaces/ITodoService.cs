using System.Collections.Generic;
using TheTodoManager.Models;

namespace TheTodoManager.BLL.Interfaces
{
    public interface ITodoService
    {
        Todo Get(int id);
        IEnumerable<Todo> GetItems();
    }
}
