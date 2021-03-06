﻿
namespace NextGenCMS.Model.constants
{
    public static class ApiHelper
    {
        /// <summary>
        /// string x-thirdparty-Id 
        /// </summary>
        public const string x_thirdparty_Id = "x-thirdparty-Id";

        /// <summary>
        /// string text_json 
        /// </summary>
        public const string text_json = "text/json";

        /// <summary>
        /// string application_json 
        /// </summary>
        public const string application_json = "application/json";

        /// <summary>
        /// string POST 
        /// </summary>
        public const string Post = "POST";

        /// <summary>
        /// String PUT
        /// </summary>
        public const string Put = "PUT";

        /// <summary>
        /// string POST 
        /// </summary>
        public const string Delete = "DELETE";

        public enum StatusCode
        {
            Success = 200,
            Exception = 201
        }
    }
}
