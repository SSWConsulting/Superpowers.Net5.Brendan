using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Superpowers.Net5.WebApi.Cqrs.TodoLists
{
    public record CreateTodoList: IRequest<int>
    {
        string Title { get; init; }
    }
}
