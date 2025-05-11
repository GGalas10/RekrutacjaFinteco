using Finteco_Core.Domain.Users;
using Finteco_Core.Enums;
using Finteco_Core.Exceptions;

namespace Finteco_Core.Domain.Tasks
{
    public class BaseTask
    {
        public Guid Id { get; set; }
        public StatusEnum Status { get; private set; }
        public int Difficult { get; private set; }
        public string Title { get; private set; }
        public Guid? UserId { get; private set; }
        public BaseTask(){}
        public BaseTask(int difficult,string title)
        {
            Id = Guid.NewGuid();
            Status = StatusEnum.ToDo;
            SetDifficult(difficult);
            SetTitle(title);
        }
        public void SetStatus(StatusEnum status)
        {
            if (Status == status)
                throw new BadRequestException("Cannot_Set_The_Same_Status");

            Status = status;
        }
        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new BadRequestException("Title_Cannot_Be_Null_Or_Empty");

            Title = title;
        }
        public void SetDifficult(int difficult)
        {
            if (Difficult == difficult)
                throw new BadRequestException("Cannot_Set_The_Same_Difficult");
            if (difficult < 1)
                throw new BadRequestException("Difficult_Cannot_Be_Less_Than_1");
            if (difficult > 5)
                throw new BadRequestException("Difficult_Cannot_Be_Greater_Than_5");

            Difficult = difficult;
        }
        public void AssignUser(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new BadRequestException("UserId_Cannot_Be_Empty");

            if (UserId == userId)
                throw new BadRequestException("Cannot_Set_The_Same_User");

            UserId = userId;
        }
        public bool UpdateBaseTask(StatusEnum? status, int? difficult, string? title)
        {
            var anyChanges = false;
            anyChanges = status == null ? false : ChangeStatus(status.Value);
            anyChanges = difficult == null ? false : ChangeDifficult(difficult.Value);
            anyChanges = title == null ? false : ChangeTitle(title);
            return anyChanges;
        }
        private bool ChangeStatus(StatusEnum status)
        {
            try
            {
                SetStatus(status);
                return true;
            }
            catch 
            {
                return false;
            }
        }
        private bool ChangeDifficult(int difficult)
        {
            try
            {
                SetDifficult(difficult);
                return true;
            }
            catch 
            {
                return false;
            }
        }
        private bool ChangeTitle(string title)
        {
            try
            {
                SetTitle(title);
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
