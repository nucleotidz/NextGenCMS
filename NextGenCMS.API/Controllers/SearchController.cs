
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using NextGenCMS.API.Filters;
using NextGenCMS.BL.interfaces;

namespace NextGenCMS.API.Controllers
{

    [RoutePrefix("api/Search")]
    [Secure]
    public class SearchController : ApiController
    {
        ISearchBL _searchBL;
        public SearchController(ISearchBL searchBL)
        {
            this._searchBL = searchBL;
        }

        [HttpGet]
        [Route("File/{searchKey}")]
        public HttpResponseMessage SearchFiles(string searchKey)
        {
            return Request.CreateResponse(HttpStatusCode.OK, (object)this._searchBL.SearchFile(searchKey));
        }


    }
}
