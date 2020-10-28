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
    public class ItemDoneHandler : IRequestHandler<ItemDone>
    {
        private readonly TodoContext _ctx;

        public ItemDoneHandler(TodoContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Unit> Handle(ItemDone request, CancellationToken cancellationToken)
        {
            var item = await _ctx.TodoItems.FindAsync(new object[] { request.Id }, cancellationToken);
            if (item == null) throw new ApplicationException($"Failed to load TodoItem {request.Id}");
            item.Done = request.IsDone;
            await _ctx.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
