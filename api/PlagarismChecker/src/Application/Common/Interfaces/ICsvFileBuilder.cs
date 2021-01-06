using PlagarismChecker.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace PlagarismChecker.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
