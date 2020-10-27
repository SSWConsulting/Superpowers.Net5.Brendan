using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Superpowers.Net5.Models.Commands
{
    public record CreateTodoList: IRequest<int>
    {
        public string Title { get; init; }
    }
}
