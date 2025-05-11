using Finteco_Core.Enums;
using Finteco_Core.Exceptions;

namespace Finteco_Core.Domain.Users
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public UserTypeEnum UserType { get; private set; }
        private User() { }
        public User(string name, UserTypeEnum userType)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetUserType(userType);
        }
        public User(Guid userId, string name, UserTypeEnum userType)
        {
            Id = userId;
            SetName(name);
            SetUserType(userType);
        }
        
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BadRequestException("Email_Cannot_Be_Null_Or_Empty");
            Name = name;
        }
        public void SetUserType(UserTypeEnum userType)
        {
            if (UserType == userType)
                throw new BadRequestException("Cannot_Set_The_Same_UserType");
            UserType = userType;
        }
        public bool UpdateUser(string name, UserTypeEnum userType)
        {
            var anyChanges = false;
            anyChanges = ChangeName(name);
            anyChanges = ChangeType(userType);
            return anyChanges;
        }
        private bool ChangeName(string email)
        {
            try
            {
                SetName(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool ChangeType(UserTypeEnum userType)
        {
            try
            {
                SetUserType(userType);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
