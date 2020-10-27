using MediatR;
using Microsoft.AspNetCore.Mvc;
using Superpowers.Net5.Models.Todo;
using Superpowers.Net5.WebApi.Cqrs.TodoLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Superpowers.Net5.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListController : ControllerBase
    {

        private readonly IMediator _mediator;

        public TodoListController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoListDto>>> All(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetTodoLists(), cancellationToken));
        }

    }
}
