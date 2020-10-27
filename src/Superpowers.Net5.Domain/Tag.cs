using System.Collections.Generic;

namespace Net5DomainModel
{
    public class Tag
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IList<TodoItem> TodoItems { get; } = new List<TodoItem>();
    }
}
