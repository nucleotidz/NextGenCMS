using System.Configuration;

namespace NextGenCMS.Model.constants
{
    public class AppConfigKeys
    {
        public static readonly string ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        public static readonly string Site = ConfigurationManager.AppSettings["Site:Name"];
    }
}
