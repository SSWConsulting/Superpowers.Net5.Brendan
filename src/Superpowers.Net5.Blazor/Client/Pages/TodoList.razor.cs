using Fluxor;
using Microsoft.AspNetCore.Components;
using Superpowers.Net5.Blazor.Client.Store.Todo;
using Superpowers.Net5.Models.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superpowers.Net5.Blazor.Client.Pages
{
    public partial class TodoList
    {

        [Inject]
        private IState<TodoState> TodoState { get; set; }

        [Inject]
        private IDispatcher Dispatcher { get; set; }


        public string NewListTitle { get; set; }
        public string NewItemTitle { get; set; }


        private void SelectList(TodoListDto list)
        {
            Dispatcher.Dispatch(new SelectListAction { SelectedList = list });
        }

        private void CreateList()
        {
            Dispatcher.Dispatch(new AddListAction { Title = NewListTitle });
        }

        private void CreateListItem()
        {
            Dispatcher.Dispatch(new AddListItemAction { ListId = TodoState.Value.SelectedList.Id, Title = NewItemTitle });
        }


        private void DeleteList(int id)
        {
            Dispatcher.Dispatch(new DeleteListAction { Id = id });
        }

        private void DeleteListItem(int id)
        {
            Dispatcher.Dispatch(new DeleteListItemAction { Id = id });
        }


        private void ItemDone(int itemId, bool done)
        {
            Dispatcher.Dispatch(new ItemDoneAction { Id = itemId, IsDone = done });
        }



    }
}
