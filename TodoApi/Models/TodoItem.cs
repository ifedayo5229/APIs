using System;
namespace TodoApi.Models
{
    public class TodoItem
    {
        public int Id {get; set;}
        public string Title {get; set;}
        public DateTime CreatedDate {get; set;}
        public string Catergory {get; set;}
        public string Name {get; set;}
        public bool IsComplete {get; set;}
    }
}
