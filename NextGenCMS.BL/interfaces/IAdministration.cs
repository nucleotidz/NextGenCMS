﻿using System;
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
        /// <param name="searchText">searchText</param>
        /// <returns>list of users</returns>
        GetUsersResponse GetUsers(string searchText);

        /// <summary>
        /// This method will fetch user details based on username
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>user details</returns>
        User GetUser(string username);

        /// <summary>
        /// This method will return all the groups
        /// </summary>
        /// <returns>list of groups</returns>
        GetGroupsResponse GetGroups();
    }
}
