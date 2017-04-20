
namespace NextGenCMS.Model.classes.administration
{
    #region Namespaces"
    using System.Collections.Generic;
    #endregion

    /// <summary>
    /// This model class is used to create a new user
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// Gets or sets userName
        /// </summary>
        /// <value>string</value>
        public string userName { get; set; }

        /// <summary>
        /// Gets or sets firstName
        /// </summary>
        /// <value>string</value>
        public string firstName { get; set; }

        /// <summary>
        /// Gets or sets lastName
        /// </summary>
        /// <value>string</value> 
        public string lastName { get; set; }

        /// <summary>
        /// Gets or sets email
        /// </summary>
        /// <value>string</value>
        public string email { get; set; }

        /// <summary>
        /// Gets or sets password
        /// </summary>
        /// <value>string</value>
        public string password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether account will be disabled or not 
        /// </summary>
        /// <value>string</value>
        public bool disableAccount { get; set; }

        /// <summary>
        /// Gets or sets quota
        /// </summary>
        /// <value>string</value>
        public string quota { get; set; }

        /// <summary>
        /// Gets or sets groups
        /// </summary>
        /// <value>string</value>     
        public string[] groups { get; set; }
    }
}
