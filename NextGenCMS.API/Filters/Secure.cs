using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Text;
using System.Web.Configuration;
using NextGenCMS.Model.constants;

namespace NextGenCMS.API.Filters
{
    public class Secure : AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Contains(Filter.Tenant))
            {
                string tenant = actionContext.Request.Headers.FirstOrDefault(header => header.Key == Filter.Tenant).Value.ToList()[0].ToString();
                HttpContext.Current.Items[Filter.Tenant] = tenant;
            }
            if (actionContext.Request.Headers.Contains(Filter.Token))
            {
                string token= actionContext.Request.Headers.FirstOrDefault(header => header.Key == Filter.Token).Value.ToList()[0].ToString();
                HttpContext.Current.Items[Filter.Token] = token;

                return true;
            }
            return false;
        }
    }
}