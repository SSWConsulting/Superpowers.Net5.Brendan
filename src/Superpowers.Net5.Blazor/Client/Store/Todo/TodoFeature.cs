using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superpowers.Net5.Blazor.Client.Store.Todo
{
    public class TodoFeature : Feature<TodoState>
    {
        public override string GetName() => "Todo";

        protected override TodoState GetInitialState() => new TodoState();
       
    }
}
