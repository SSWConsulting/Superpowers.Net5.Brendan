using MediatR;
using Microsoft.AspNetCore.Mvc.Formatters;
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
    public class CreateTodoListItemHandler : IRequestHandler<CreateTodoListItem, int>
    {
        private readonly TodoContext _ctx;

        public CreateTodoListItemHandler(TodoContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> Handle(CreateTodoListItem request, CancellationToken cancellationToken)
        {
            var todoList = await _ctx.TodoLists.FindAsync(new object[] { request.ListId }, cancellationToken);
            if (todoList == null) throw new ApplicationException($"failed to load TodoList {request.ListId}");

            var newItem = new TodoItem() {
                List = todoList,
                Title = request.Title,
                Priority = PriorityLevel.Medium
            };

            _ctx.TodoItems.Add(newItem);
            await _ctx.SaveChangesAsync(cancellationToken);

            return newItem.Id;
        }
    }
}
