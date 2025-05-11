using Finteco_Core.Enums;
using Finteco_Core.Exceptions;

namespace Finteco_Core.Domain.Tasks
{
    public class ImplementationTask : BaseTask
    {
        public string TaskDescription { get; private set; }
        private ImplementationTask() : base() { }
        public ImplementationTask(string taskDescription, int difficult, string title) : base(difficult, title)
        {
            SetTaskDescription(taskDescription);
        }
        public void SetTaskDescription(string taskDescription)
        {
            if (string.IsNullOrWhiteSpace(taskDescription))
                throw new BadRequestException("TaskDescription_Cannot_Be_Null_Or_Empty");

            if (taskDescription.Length > 1000)
                throw new BadRequestException("Length_Of_TaskDescription_Cannot_Be_Longer_Than_1000_Characters");

            TaskDescription = taskDescription;
        }
        public bool UpdateImplementationTask(string? taskDescription, StatusEnum? status, int? difficult, string? title)
        {
            var anyChanges = false;
            anyChanges = string.IsNullOrWhiteSpace(taskDescription) ? false : ChangeTaskDescription(taskDescription);
            anyChanges = base.UpdateBaseTask(status, difficult, title);
            return anyChanges;
        }
        private bool ChangeTaskDescription(string taskDescription)
        {
            try
            {
                SetTaskDescription(taskDescription);
                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Length_Of_TaskDescription_Cannot_Be_Longer_Than_1000_Characters")
                    throw ex;
                return false;
            }
        }
    }
}
