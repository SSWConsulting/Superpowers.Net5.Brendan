using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Superpowers.Net5.Ef;
using Superpowers.Net5.Models.Notifications;
using Superpowers.Net5.Models.Todo;
using Superpowers.Net5.WebApi.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Todo;

namespace Superpowers.Net5.WebApi.Cqrs.TodoLists
{
    public class TodoItemDoneNotificationHandler : INotificationHandler<TodoItemDoneNotification>
    {
        private readonly IDbContextFactory<TodoContext> _ctxFactory;
        private readonly TodoService.TodoServiceClient _grpcClient;
        private readonly IHubContext<TodoHub> _hubContext;


        public TodoItemDoneNotificationHandler(IDbContextFactory<TodoContext> ctxFactory, TodoService.TodoServiceClient grpcClient, IHubContext<TodoHub> hubContext)
        {
            _ctxFactory = ctxFactory;
            _grpcClient = grpcClient;
            _hubContext = hubContext;
        }


        public async Task Handle(TodoItemDoneNotification notification, CancellationToken cancellationToken)
        {
            using (var ctx = _ctxFactory.CreateDbContext())
            {
                // load all lists.
                var allLists = await GetAllLists(ctx);
                // send updated lists to all connected SignalR clients
                await _hubContext.Clients.All.SendAsync("ListsChanged", allLists);

                if (notification.IsDone)
                {

                    // send via grpc
                    await _grpcClient.SendItemDoneAsync(new TodoItemDone { Id = notification.Id, Title = notification.Title });
                }

            }
        }


        public async Task<IEnumerable<TodoListDto>> GetAllLists(TodoContext ctx)
        {
            return await ctx.TodoLists
                .Select(l => new TodoListDto()
                {
                    Id = l.Id,
                    Title = l.Title,
                    Items = l.Items.Select(i => new TodoItemDto()
                    {
                        Id = i.Id,
                        ListId = i.ListId,
                        Done = i.Done,
                        Note = i.Note,
                        Priority = new PriorityLevelDto() { Value = (int)i.Priority, Name = i.Priority.ToString() },
                        Title = i.Title
                    })
                }).ToListAsync();
        }


    }
}
