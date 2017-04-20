
namespace NextGenCMS.BL.classes
{
    #region Namespaces
    using System;
    using System.Web;
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
        public User CreateUser(CreateUserRequest createUser)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Post(ServiceUrl.CreateUser + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(createUser));
            }

            User response = JsonConvert.DeserializeObject<User>(data);
            return response;
        }

        /// <summary>
        /// This method will return all the users
        /// </summary>
        /// <returns>list of users</returns>
        public GetUsersResponse GetUsers()
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.GetUsers + HttpContext.Current.Items[Filter.Token]);
            }

            GetUsersResponse response = JsonConvert.DeserializeObject<GetUsersResponse>(data);
            return response;
        }
        #endregion

        #region "Groups"
        /// <summary>
        /// This method will return all the users
        /// </summary>
        /// <returns>list of users</returns>
        public GetGroupsResponse GetGroups()
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.GetGroups + HttpContext.Current.Items[Filter.Token]);
            }

            GetGroupsResponse response = JsonConvert.DeserializeObject<GetGroupsResponse>(data);
            return response;
        }
        #endregion

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
                }
            }

            this._disposed = true;
        }
        #endregion
    }
}
