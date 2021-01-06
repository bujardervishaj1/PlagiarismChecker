using PlagarismChecker.Application.Common.Mappings;
using PlagarismChecker.Domain.Entities;

namespace PlagarismChecker.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
