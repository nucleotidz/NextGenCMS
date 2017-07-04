
namespace NextGenCMS.BL.classes
{
    #region Namespaces
    using System;
    using System.Web;
    using System.Linq;
    using System.Collections.Generic;
    using System.Net;
    using System.IO;
    using Newtonsoft.Json;
    #endregion

    #region "NextGenCMS Namespaces"
    using NextGenCMS.APIHelper.interfaces;
    using NextGenCMS.DL.interfaces;
    using NextGenCMS.Model.classes;
    using NextGenCMS.BL.interfaces;
    using NextGenCMS.Model.constants;
    using NextGenCMS.Model.classes.administration;
    using NextGenCMS.Model.classes.administration.GetUsers;
    using NextGenCMS.Model.classes.administration.CreateUser;
    using NextGenCMS.Model.classes.permissions;
    using NextGenCMS.Model.classes.administration.GetGroups;
    #endregion

    public class Administration : IAdministration
    {
        #region "Private fields"
        /// <summary>
        /// disposed is used to reallocate memory of Unused Objects
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// data layer object
        /// </summary>
        private readonly IAdministrationRepository _repository;

        /// <summary>
        /// api helper object
        /// </summary>
        private readonly IAPIHelper _apiHelper;
        #endregion

        #region Constructor
        /// <summary>
        /// COnstructor to initialize objects
        /// </summary>
        /// <param name="repository">IAuthenticationRepository</param>
        /// <param name="apiHelper">IAPIHelper</param>
        public Administration(IAdministrationRepository repository, IAPIHelper apiHelper)
        {
            _repository = repository;
            _apiHelper = apiHelper;
        }
        #endregion

        #region "Users"
        /// <summary>
        /// This method delete the ticket and logout the user
        /// </summary>
        /// <param name="createUser">createUser</param>
        /// <returns>string</returns>
        public object CreateUser(CreateUserRequest createUser)
        {
            WebResponseModel response = new WebResponseModel();
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                response = this._apiHelper.Submit(ServiceUrl.CreateUser + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(createUser.User));
            }

            if (response.status == NextGenCMS.Model.constants.ApiHelper.StatusCode.Success)
            {
                UserWithGroups data = JsonConvert.DeserializeObject<UserWithGroups>(response.message);

                var obj = new ApiResponseModel<UserWithGroups>
                {
                    Status = response.status,
                    Result = data
                };

                response = this._apiHelper.Submit(ServiceUrl.AddUserRole + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(createUser.UserRole));
                return obj;
            }
            else
            {
                ResponseError data = JsonConvert.DeserializeObject<ResponseError>(response.message);

                var obj = new ApiResponseModel<ResponseError>
                {
                    Status = response.status,
                    Result = data
                };
                return obj;
            }
        }

        /// <summary>
        /// This method will return all the users
        /// </summary>
        /// <param name="searchText">searchText</param>
        /// <returns>list of users</returns>
        public GetUsersResponse GetUsers(string searchText, string username)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.GetUsers + HttpContext.Current.Items[Filter.Token]);
            }

            GetUsersResponse response = JsonConvert.DeserializeObject<GetUsersResponse>(data);

            if (string.IsNullOrWhiteSpace(searchText))
            {
                response.people = response.people.Where(user => user.userName != username && !user.isDeleted).OrderBy(x => x.firstName).ToList();
            }
            else
            {
                response.people = response.people.Where(user => (user.firstName.IndexOf(searchText) != -1 ||
                                                                user.lastName.IndexOf(searchText) != -1 ||
                                                                user.userName.IndexOf(searchText) != -1) &&
                                                                user.userName != username && !user.isDeleted).OrderBy(x => x.firstName).ToList();
            }

            //get site users
            var siteUsers = GetSiteUsers();
            //filter usernames of site users
            var userList = siteUsers.Select(user => user.authority.userName);
            //filter search users - only site users should be fetched
            response.people = response.people.Where(user => userList.Contains(user.userName)).ToList();
            response.people.ForEach(user =>
                {
                    user.role = siteUsers.FirstOrDefault(suser => suser.authority.userName == user.userName).role.Replace("Site", string.Empty);
                });

            response.paging.totalItems = response.people.Count();
            return response;
        }

        /// <summary>
        /// This method will fetch user details based on username
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>user details</returns>
        public UserWithGroups GetUser(string username)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.GetUser + username + "?groups=true&alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }

            UserWithGroups response = JsonConvert.DeserializeObject<UserWithGroups>(data);

            return response;
        }

        public bool DeleteUser(List<string> users)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                foreach (var username in users)
                {
                    data = this._apiHelper.Delete(ServiceUrl.DeleteUser + username + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
                }
            }

            return true;
        }

        /// <summary>
        /// This method will fetch user sites based on username
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>user details</returns>
        public UserSites GetUserSites(string username)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.GetUserSites + username + "/sites?roles=user&alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }

            List<UserSites> response = JsonConvert.DeserializeObject<List<UserSites>>(data);

            return response.FirstOrDefault(site => site.shortName == AppConfigKeys.Site);
        }

        /// <summary>
        /// This method will fetch user of site
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>user details</returns>
        public List<SiteUsers> GetSiteUsers(string searchText = "")
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                if (string.IsNullOrEmpty(searchText))
                    data = this._apiHelper.Get(ServiceUrl.GetSiteUsers + ServiceUrl.AlfTicket + HttpContext.Current.Items[Filter.Token]);
                else
                    data = this._apiHelper.Get(ServiceUrl.GetSiteUsers + searchText + ServiceUrl.AlfTicket + HttpContext.Current.Items[Filter.Token]);
            }

            var response = JsonConvert.DeserializeObject<List<SiteUsers>>(data);
            return response;
        }
        #endregion

        #region "Groups"
        /// <summary>
        /// This method will return all the groups
        /// </summary>
        /// <returns>list of groups</returns>
        public GetGroupsResponse GetGroups()
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.GetGroups + HttpContext.Current.Items[Filter.Token]);
            }

            GetGroupsResponse response = JsonConvert.DeserializeObject<GetGroupsResponse>(data);
            //response.data = response.data.Where(group => group.zones.Contains("APP.DEFAULT")).ToList();
            return response;
        }

        /// <summary>
        /// This method will return all the groups
        /// </summary>
        /// <returns>list of groups</returns>
        public GetGroupsResponse SearchGroups(string searchText)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.SearchGroups + searchText + ServiceUrl.AlfTicket + HttpContext.Current.Items[Filter.Token]);
            }

            GetGroupsResponse response = JsonConvert.DeserializeObject<GetGroupsResponse>(data);
            //response.data = response.data.Where(group => group.zones.Contains("APP.DEFAULT")).ToList();
            return response;
        }

        /// <summary>
        /// This method delete the ticket and logout the user
        /// </summary>
        /// <param name="createUser">createUser</param>
        /// <returns>string</returns>
        public WebResponseModel CreateGroup(Group group)
        {
            WebResponseModel response = new WebResponseModel();
            GroupExistResponse responseNotExist = new GroupExistResponse();
            GetGroupsResponse responseExist = new GetGroupsResponse();
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                string uri = System.Web.HttpUtility.UrlPathEncode(ServiceUrl.Groups + group.fullName + ServiceUrl.GroupExistSuffix + ServiceUrl.AlfTicket + HttpContext.Current.Items[Filter.Token]);
                try
                {
                    var data = this._apiHelper.Get(uri);
                    responseExist = JsonConvert.DeserializeObject<GetGroupsResponse>(data);
                }
                catch (WebException wex)
                {
                    if (wex.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse)wex.Response)
                        {
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                string message = reader.ReadToEnd().Trim();
                                responseNotExist = JsonConvert.DeserializeObject<GroupExistResponse>(message);
                            }
                        }
                    }
                }
                finally
                {
                    if (responseNotExist.status != null && responseNotExist.status.code == 404)
                    {
                        response = this._apiHelper.Submit(ServiceUrl.AddGroups + group.fullName + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(group));
                    }
                    else
                    {
                        response.status = NextGenCMS.Model.constants.ApiHelper.StatusCode.Exception;
                    }
                }
            }
            return response;
        }

        public bool UpdateGroup(Group group)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Put(ServiceUrl.Groups + group.fullName + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(group));
            }

            return true;
        }

        public bool DeleteGroup(List<string> groups)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                foreach (var group in groups)
                {
                    data = this._apiHelper.Delete(ServiceUrl.Groups + group + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
                }
            }

            return true;
        }

        public void AddGroupUser(string username)
        {
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                var response = this._apiHelper.Submit(ServiceUrl.AddDeleteGroupUser + username + ServiceUrl.Alf_Ticket + HttpContext.Current.Items[Filter.Token], "");
            }
        }

        public void DeleteGroupUser(string username)
        {
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                var response = this._apiHelper.Delete(ServiceUrl.AddDeleteGroupUser + username + ServiceUrl.Alf_Ticket + HttpContext.Current.Items[Filter.Token]);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public Permissions GetPermissions(string nodeId)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.Permissions + nodeId + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }

            Permissions response = JsonConvert.DeserializeObject<Permissions>(data);
            response.direct = response.direct.OrderBy(x => x.authority.displayName).ToList();
            return response;
        }

        /// <summary>
        /// Folder permissions - This method will return users and groups that can be added
        /// </summary>
        /// <param name="searchText">searchText</param>
        /// <returns></returns>
        public SiteGroupAndUsers SearchUserAndGroups(string searchText)
        {
            var users = GetSiteUsers(searchText);
            var groups = SearchGroups(searchText);
            var response = new SiteGroupAndUsers { authorities = new List<NextGenCMS.Model.classes.permissions.Authority>() };
            users.ForEach(user =>
            {
                var authority = new NextGenCMS.Model.classes.permissions.Authority();
                authority.authorityType = user.authority.authorityType;
                authority.displayName = user.authority.firstName + (string.IsNullOrWhiteSpace(user.authority.lastName) ? "" : " " + user.authority.lastName);
                authority.fullName = user.authority.fullName;
                authority.name = user.authority.userName;
                authority.role = user.role;
                response.authorities.Add(authority);
            });

            groups.data.Where(x => x.shortName != "site_" + AppConfigKeys.Site).ToList().ForEach(group =>
            {
                var authority = new NextGenCMS.Model.classes.permissions.Authority();
                authority.authorityType = group.authorityType;
                authority.displayName = group.displayName;
                authority.fullName = group.fullName;
                authority.role = group.zones.FirstOrDefault();
                response.authorities.Add(authority);
            });
            response.authorities = response.authorities.OrderBy(x => x.displayName).ToList();
            return response;
        }

        public bool SavePermissions(SavePermission permissions)
        {
            WebResponseModel response = new WebResponseModel();
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                response = this._apiHelper.Submit(ServiceUrl.Permissions + permissions.nodeId + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(permissions));
            }

            return true;
        }

        #region Dispose
        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Overriding Dispose method
        /// </summary>
        /// <param name="disposing">disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _repository.Dispose();
                    _apiHelper.Dispose();
                }
            }

            this._disposed = true;
        }
        #endregion
    }
}
