using NextGenCMS.Model.classes.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.BL.interfaces
{
    public interface ISearchBL
    {
        dynamic SearchFile(string searchKey);
    }
}
