﻿using System;
using System.Collections.Generic;
using NextGenCMS.Model.classes.administration;
using NextGenCMS.Model.classes.administration.GetUsers;
using NextGenCMS.Model.classes.administration.CreateUser;

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
        GetUsersResponse GetUsers(string searchText, string username);

        /// <summary>
        /// This method will fetch user details based on username
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>user details</returns>
        UserWithGroups GetUser(string username);

        bool DeleteUser(List<string> users);

        /// <summary>
        /// This method will fetch user sites based on username
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>user sites with role</returns>
        UserSites GetUserSites(string username);

        /// <summary>
        /// This method will fetch user of site
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>user details</returns>
        List<SiteUsers> GetSiteUsers();

        /// <summary>
        /// This method will return all the groups
        /// </summary>
        /// <returns>list of groups</returns>
        GetGroupsResponse GetGroups();
    }
}
