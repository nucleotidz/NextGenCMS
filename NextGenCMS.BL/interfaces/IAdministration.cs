using System;
using NextGenCMS.Model.classes.administration;
using NextGenCMS.Model.classes.administration.GetUsers;

namespace NextGenCMS.BL.interfaces
{
    public interface IAdministration : IDisposable
    {
        /// <summary>
        /// This method delete the ticket and logout the user
        /// </summary>
        /// <param name="createUser">createUser</param>
        /// <returns>string</returns>
        object CreateUser(CreateUserRequest createUser);

        /// <summary>
        /// This method will return all the users
        /// </summary>
        /// <returns>list of users</returns>
        GetUsersResponse GetUsers();

        /// <summary>
        /// This method will return all the users
        /// </summary>
        /// <returns>list of users</returns>
        GetGroupsResponse GetGroups();
    }
}
