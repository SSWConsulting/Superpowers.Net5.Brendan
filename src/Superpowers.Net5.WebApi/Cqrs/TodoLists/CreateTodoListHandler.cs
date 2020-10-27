using MediatR;
using Net5DomainModel;
using Superpowers.Net5.Ef;
using Superpowers.Net5.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Superpowers.Net5.WebApi.Cqrs.TodoLists
{
    public class CreateTodoListHandler : IRequestHandler<CreateTodoList, int>
    {
        private readonly TodoContext _ctx;

        public CreateTodoListHandler(TodoContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> Handle(CreateTodoList request, CancellationToken cancellationToken)
        {
            var newTodo = new TodoList()
            {
                Title = request.Title,
                Colour = "#FF0000"
            };

            _ctx.TodoLists.Add(newTodo);
            await _ctx.SaveChangesAsync();
            return newTodo.Id;
        }
    }
}
