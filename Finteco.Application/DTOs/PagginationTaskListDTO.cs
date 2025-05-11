namespace Finteco.Application.DTOs
{
    public class PagginationTaskListDTO
    {
        public int page { get; set; }
        public int maxPage { get; set; }
        public List<TaskListDTO> tasks { get; set; }
    }
}
