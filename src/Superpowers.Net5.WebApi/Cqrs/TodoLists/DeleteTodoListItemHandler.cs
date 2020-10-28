using MediatR;
using Superpowers.Net5.Ef;
using Superpowers.Net5.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Superpowers.Net5.WebApi.Cqrs.TodoLists
{
    public class DeleteTodoListItemHandler : IRequestHandler<DeleteTodoListItem>
    {
        private readonly TodoContext _ctx;

        public DeleteTodoListItemHandler(TodoContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Unit> Handle(DeleteTodoListItem request, CancellationToken cancellationToken)
        {
            var toDelete = await _ctx.TodoItems.FindAsync(new object[] { request.Id }, cancellationToken);
            if (toDelete == null) throw new ApplicationException($"failed to load todo item {request.Id}");
            _ctx.TodoItems.Remove(toDelete);
            await _ctx.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
