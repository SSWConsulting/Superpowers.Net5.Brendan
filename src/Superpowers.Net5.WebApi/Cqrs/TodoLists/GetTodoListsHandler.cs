using MediatR;
using Superpowers.Net5.Ef;
using Superpowers.Net5.Models.Todo;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Superpowers.Net5.Models.Queries;

namespace Superpowers.Net5.WebApi.Cqrs.TodoLists
{
    public class GetTodoListsHandler : IRequestHandler<GetTodoLists, IEnumerable<TodoListDto>>
    {
        private readonly TodoContext _ctx;

        public GetTodoListsHandler(TodoContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<TodoListDto>> Handle(GetTodoLists request, CancellationToken cancellationToken)
        {
            return await _ctx.TodoLists
                .Select(l => new TodoListDto() {
                    Id = l.Id,
                    Title = l.Title,
                    Items = l.Items.Select(i => new TodoItemDto() {
                        Id = i.Id,
                        ListId = i.ListId,
                        Done = i.Done,
                        Note = i.Note,
                        Priority = new PriorityLevelDto() { Value=(int)i.Priority, Name=i.Priority.ToString() },
                        Title = i.Title
                    })
                }).ToListAsync();
        }
    }

}
