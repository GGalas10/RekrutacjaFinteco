using Finteco_Core.Enums;
using Finteco_Core.Exceptions;

namespace Finteco_Core.Domain.Tasks
{
    public class MaintenanceTask : BaseTask
    {
        public DateTime Deadline { get; private set; }
        public string ServiceList { get; private set; }
        public string ServerList { get; private set; }
        private MaintenanceTask() : base() { }
        public MaintenanceTask(DateTime deadline, string serviceList, string serverList, int difficult, string title) : base(difficult, title)
        {
            SetDeadline(deadline);
            SetServiceList(serviceList);
            SetServerList(serverList);
        }
        public void SetDeadline(DateTime deadline)
        {
            if (deadline < DateTime.Now)
                throw new BadRequestException("Deadline_Cannot_Be_Earlier_Than_Today");
            Deadline = deadline;
        }
        public void SetServiceList(string serviceList)
        {
            if (string.IsNullOrWhiteSpace(serviceList))
                throw new BadRequestException("ServiceList_Cannot_Be_Null_Or_Empty");

            if (serviceList.Length > 400)
                throw new BadRequestException("Length_Of_ServiceList_Cannot_Be_Longer_Than_400_Characters");

            ServiceList = serviceList;
        }
        public void SetServerList(string serverList)
        {
            if (string.IsNullOrWhiteSpace(serverList))
                throw new BadRequestException("ServerList_Cannot_Be_Null_Or_Empty");

            if (serverList.Length > 400)
                throw new BadRequestException("Length_Of_ServerList_Cannot_Be_Longer_Than_400_Characters");

            ServerList = serverList;
        }
        public bool UpdateMaintenanceTask(DateTime? deadline, string?  serviceList, string? serverList, StatusEnum? status, int? difficult, string? title)
        {
            var anyChanges = false;
            anyChanges = deadline == null ? false : ChangeDeadLine(deadline.Value);
            anyChanges = serviceList == null ? false : ChangeServiceList(serviceList);
            anyChanges = serverList == null ? false : ChangeServerList(serverList);
            anyChanges = base.UpdateBaseTask(status, difficult, title);
            return anyChanges;
        }
        private bool ChangeDeadLine(DateTime deadline)
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
        private bool ChangeServiceList(string serviceList)
        {
            try
            {
                SetServerList(serviceList);
                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Length_Of_ServiceList_Cannot_Be_Longer_Than_400_Characters")
                    throw ex;
                return false;
            }
        }
        private bool ChangeServerList(string serverList)
        {
            try
            {
                SetServerList(serverList);
                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Length_Of_ServerList_Cannot_Be_Longer_Than_400_Characters")
                    throw ex;
                return false;
            }
        }
    }
}
