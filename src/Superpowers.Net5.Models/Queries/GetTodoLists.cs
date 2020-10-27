using MediatR;
using Superpowers.Net5.Models.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superpowers.Net5.Models.Queries
{
    public record GetTodoLists : IRequest<IEnumerable<TodoListDto>>
    {
    }
}
