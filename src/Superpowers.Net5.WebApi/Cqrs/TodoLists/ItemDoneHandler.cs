using MediatR;
using Superpowers.Net5.Ef;
using Superpowers.Net5.Models.Commands;
using Superpowers.Net5.Models.Notifications;
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
        private readonly IMediator _mediator;

        public ItemDoneHandler(TodoContext ctx, IMediator mediator)
        {
            _ctx = ctx;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(ItemDone request, CancellationToken cancellationToken)
        {
            var item = await _ctx.TodoItems.FindAsync(new object[] { request.Id }, cancellationToken);
            if (item == null) throw new ApplicationException($"Failed to load TodoItem {request.Id}");
            item.Done = request.IsDone;
            await _ctx.SaveChangesAsync();

            await _mediator.Publish(new TodoItemDoneNotification { 
                Id = request.Id, 
                IsDone = request.IsDone,
                Title = item.Title
            });

            return Unit.Value;
        }
    }
}
