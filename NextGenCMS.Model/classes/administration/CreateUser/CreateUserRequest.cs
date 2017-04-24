
namespace NextGenCMS.Model.classes.administration.CreateUser
{
    #region Namespaces"
    using System.Collections.Generic;
    #endregion

    /// <summary>
    /// This model class is used to create a new user
    /// </summary>
    public class CreateUserRequest
    {
        public AddUser User { get; set; }
        public AddUserRole UserRole { get; set; }
    }
}
