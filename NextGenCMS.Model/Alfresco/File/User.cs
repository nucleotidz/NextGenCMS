using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.Alfresco.File
{

    public class User
    {
        public bool Delete { get; set; }
        public bool Write { get; set; }
        public bool CancelCheckOut { get; set; }
        public bool ChangePermissions { get; set; }
        public bool CreateChildren { get; set; }
        public bool Unlock { get; set; }
    }

    public class Permissions
    {
        public bool inherited { get; set; }
        public List<string> roles { get; set; }
        public User user { get; set; }
    }

    public class CmCreator
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string displayName { get; set; }
        public string userName { get; set; }
    }

    public class CmModifier
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string displayName { get; set; }
        public string userName { get; set; }
    }

    public class CmCreated
    {
        public string iso8601 { get; set; }
        public string value { get; set; }
    }

    public class CmModified
    {
        public string iso8601 { get; set; }
        public string value { get; set; }
    }

    public class Properties
    {
        public string cm_title { get; set; }
        public CmCreator cm_creator { get; set; }
        public CmModifier cm_modifier { get; set; }
        public CmCreated cm_created { get; set; }
        public string cm_name { get; set; }
        public object sys_store_protocol { get; set; }
        public object sys_node_dbid { get; set; }
        public object sys_store_identifier { get; set; }
        public object sys_locale { get; set; }
        public CmModified cm_modified { get; set; }
        public string cm_description { get; set; }
        public object sys_node_uuid { get; set; }
    }

    public class Parent
    {
        public bool isLink { get; set; }
        public string nodeRef { get; set; }
        public Permissions permissions { get; set; }
        public bool isLocked { get; set; }
        public List<string> aspects { get; set; }
        public bool isContainer { get; set; }
        public string type { get; set; }
        public Properties properties { get; set; }
    }

    public class ImportFormats
    {
        public string application_vnd_openxmlformats_officedocument_presentationml_presentation { get; set; }
        public string application_vnd_ms_powerpoint { get; set; }
        public string text_tab_separated_values { get; set; }
        public string application_vnd_sun_xml_writer { get; set; }
        public string application_vnd_ms_excel { get; set; }
        public string application_vnd_openxmlformats_officedocument_spreadsheetml_sheet { get; set; }
        public string application_rtf { get; set; }
        public string application_msword { get; set; }
        public string application_vnd_oasis_opendocument_text { get; set; }
        public string text_plain { get; set; }
        public string text_csv { get; set; }
        public string application_x_vnd_oasis_opendocument_spreadsheet { get; set; }
        public string application_vnd_oasis_opendocument_spreadsheet { get; set; }
        public string application_vnd_openxmlformats_officedocument_wordprocessingml_document { get; set; }
    }

    public class Googledocs
    {
        public bool enabled { get; set; }
        public ImportFormats importFormats { get; set; }
    }

    public class Aos
    {
        public string baseUrl { get; set; }
    }

    public class Custom
    {
        public Googledocs googledocs { get; set; }
        public Aos aos { get; set; }
        public object vtiServer { get; set; }
    }

    public class ItemCounts
    {
        public int folders { get; set; }
        public int documents { get; set; }
    }

    public class Metadata
    {
        public string repositoryId { get; set; }
        public string container { get; set; }
        public Parent parent { get; set; }
        public Custom custom { get; set; }
        public bool onlineEditing { get; set; }
        public ItemCounts itemCounts { get; set; }
        public string workingCopyLabel { get; set; }
    }

    public class User2
    {
        public bool Delete { get; set; }
        public bool Write { get; set; }
        public bool CancelCheckOut { get; set; }
        public bool ChangePermissions { get; set; }
        public bool CreateChildren { get; set; }
        public bool Unlock { get; set; }
    }

    public class Permissions2
    {
        public bool inherited { get; set; }
        public List<string> roles { get; set; }
        public User2 user { get; set; }
    }

    public class CmCreator2
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string displayName { get; set; }
        public string userName { get; set; }
    }

    public class CmModifier2
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string displayName { get; set; }
        public string userName { get; set; }
    }

    public class CmCreated2
    {
        public string iso8601 { get; set; }
        public string value { get; set; }
    }

    public class CmModified2
    {
        public string iso8601 { get; set; }
        public string value { get; set; }
    }

    public class Properties2
    {
        public string cm_title { get; set; }
        public CmCreator2 cm_creator { get; set; }
        public CmModifier2 cm_modifier { get; set; }
        public CmCreated2 cm_created { get; set; }
        public string cm_name { get; set; }
        public object sys_store_protocol { get; set; }
        public object sys_node_dbid { get; set; }
        public object sys_store_identifier { get; set; }
        public object sys_locale { get; set; }
        public CmModified2 cm_modified { get; set; }
        public string cm_description { get; set; }
        public object sys_node_uuid { get; set; }
        public string cm_autoVersion { get; set; }
        public string cm_versionType { get; set; }
        public string cm_versionLabel { get; set; }
        public string cm_autoVersionOnUpdateProps { get; set; }
        public object cm_content { get; set; }
        public string cm_author { get; set; }
        public string cm_initialVersion { get; set; }
    }

    public class Node
    {
        public bool isLink { get; set; }
        public string nodeRef { get; set; }
        public Permissions2 permissions { get; set; }
        public bool isLocked { get; set; }
        public List<string> aspects { get; set; }
        public bool isContainer { get; set; }
        public string type { get; set; }
        public Properties2 properties { get; set; }
        public string encoding { get; set; }
        public string contentURL { get; set; }
        public int? size { get; set; }
        public string mimetype { get; set; }
        public string mimetypeDisplayName { get; set; }
    }

    public class Likes
    {
        public bool isLiked { get; set; }
        public int totalLikes { get; set; }
    }

    public class Site
    {
        public string name { get; set; }
        public string title { get; set; }
        public string preset { get; set; }
    }

    public class Container
    {
        public string name { get; set; }
        public string type { get; set; }
        public string nodeRef { get; set; }
    }

    public class Parent2
    {
    }

    public class Location
    {
        public string repositoryId { get; set; }
        public Site site { get; set; }
        public Container container { get; set; }
        public string path { get; set; }
        public string repoPath { get; set; }
        public string file { get; set; }
        public Parent2 parent { get; set; }
    }

    public class Item
    {
        public Node node { get; set; }
        public string version { get; set; }
        public string webdavUrl { get; set; }
        public Likes likes { get; set; }
        public Location location { get; set; }
    }

    public class RootObject
    {
        public int totalRecords { get; set; }
        public int startIndex { get; set; }
        public Metadata metadata { get; set; }
        public List<Item> items { get; set; }
    }

}
