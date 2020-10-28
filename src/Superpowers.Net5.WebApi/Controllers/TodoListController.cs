using MediatR;
using Microsoft.AspNetCore.Mvc;
using Superpowers.Net5.Models.Commands;
using Superpowers.Net5.Models.Queries;
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


        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<TodoListDto>>> Create(CreateTodoList command, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        [Route("CreateItem")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<TodoListDto>>> CreateItem(CreateTodoListItem command, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteTodoList() { Id = id });
            return Ok();
        }


        [HttpDelete]
        [Route("item/{id}")]
        public async Task<ActionResult> DeleteItem(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteTodoListItem() { Id = id });
            return Ok();
        }


        [HttpPut]
        [Route("itemdone")]
        public async Task<ActionResult> ItemDone(ItemDone command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

    }
}
