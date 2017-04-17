

namespace NextGenCMS.UI.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class Secure : System.Web.Mvc.AuthorizeAttribute
    { /// <param name="filterContext">AuthorizationContext</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["SessionContext"] == null)
                filterContext.Result = new RedirectResult("~/Security/Login");
        }
    }
}