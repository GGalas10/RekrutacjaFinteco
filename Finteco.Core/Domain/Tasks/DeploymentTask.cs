using Finteco_Core.Enums;
using Finteco_Core.Exceptions;

namespace Finteco_Core.Domain.Tasks
{
    public class DeploymentTask : BaseTask
    {
        public DateTime Deadline { get; private set; }
        public string DeploymentScope { get; private set; }
        private DeploymentTask() : base() { }
        public DeploymentTask(DateTime deadline, string deploymentScope,TypeEnum type, int difficult) : base(type, difficult)
        {
            SetDeadline(deadline);
            SetDeploymentScope(deploymentScope);
        }
        public void SetDeploymentScope(string deploymentScope)
        {
            if (string.IsNullOrWhiteSpace(deploymentScope))
                throw new BadRequestException("Scope_Cannot_Be_Null_Or_Empty");

            if (deploymentScope.Length > 400)
                throw new BadRequestException("Length_Of_Scope_Cannot_Be_Longer_Than_400_Characters");

            DeploymentScope = deploymentScope;
        }
        public void SetDeadline(DateTime deadline)
        {
            if (deadline < DateTime.Now)
                throw new BadRequestException("Deadline_Cannot_Be_Earlier_Than_Today");
            Deadline = deadline;
        }
        public bool UpdateDeploymentTask(string? deploymentTask, DateTime? deadline, TypeEnum? type, StatusEnum? status, int? difficult, string? title)
        {
            
            var anyChanges = false;
            anyChanges = deploymentTask == null ? false : ChangeScope(deploymentTask);
            anyChanges = deadline == null ? false : ChangeDeadline(deadline.Value);
            anyChanges = base.UpdateBaseTask(type, status, difficult, title);
            return anyChanges;
        }
        private bool ChangeScope(string scope)
        {
            try
            {
                SetDeploymentScope(scope);
                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Length_Of_Scope_Cannot_Be_Longer_Than_400_Characters")
                    throw ex;
                return false;
            }
        }
        private bool ChangeDeadline(DateTime deadline)
        {
            try
            {
                SetDeadline(deadline);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
