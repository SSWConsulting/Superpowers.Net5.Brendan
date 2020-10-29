using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo;

namespace SuperPowers.Net5.GRPC.gRPC
{
    public class TodoService: Todo.TodoService.TodoServiceBase
    {
        private readonly ILogger<TodoService> _logger;

        public TodoService(ILogger<TodoService> logger)
        {
            _logger = logger;
        }

        public override Task<TodoItemDoneReceived> SendItemDone(TodoItemDone request, ServerCallContext context)
        {
            _logger.LogInformation($"ITEM DONE: {request.Title }");

            return Task.FromResult(new TodoItemDoneReceived());
        }
    }
}
