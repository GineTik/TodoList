namespace TodoList.Core.DTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public bool Completed { get; set; }
        public string Text { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
