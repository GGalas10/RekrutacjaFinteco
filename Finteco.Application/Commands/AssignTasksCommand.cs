namespace Finteco.Application.Commands
{
    public class AssignTasksCommand
    {
        public Guid userId { get; set; }
        public List<Guid> tasksId { get; set; }
    }
}
