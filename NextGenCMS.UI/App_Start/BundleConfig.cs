﻿using System.Web;
using System.Web.Optimization;

namespace NextGenCMS.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            LoadJavaScripts(bundles);
            LoadStyleSheets(bundles);
        }

        private static void LoadStyleSheets(BundleCollection bundles)
        {
            LoadBootStarpStyle(bundles);
            LoadBootGloabalStyle(bundles);
            LoadKendoStyle(bundles);
            LoadFactory(bundles);
        }

        private static void LoadJavaScripts(BundleCollection bundles)
        {
            LoadJquery(bundles);
            LoadAngular(bundles);
            LoadBoostrap(bundles);
            LoadKendo(bundles);
            LoadController(bundles);
            LoadConstant(bundles);
            LoadAPI(bundles);
            LoadUnderscore(bundles);
            LoadDirective(bundles);
            LoadService(bundles);
        }

        #region Javascripts
        private static void LoadJquery(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Jquery/js").Include(
                "~/Scripts/Jquery/jquery-1.10.2.min.js"
            ));
        }

        private static void LoadAngular(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Angular/js").Include
             (
                "~/Scripts/Angular/angular.min.js",
                "~/Scripts/Angular/angular-resource.min.js",
                "~/Scripts/Angular/angular-ui-router.js",
                "~/Scripts/Angular/app.js",
                "~/Scripts/Angular/route.js",
                "~/Scripts/Angular/underscore.js"
             ));
        }
        private static void LoadKendo(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/kendo/js").Include(
                "~/Scripts/kendo/kendo.all.min.js",
                "~/Scripts/kendo/jszip.min.js"
            ));
        }
        private static void LoadController(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/controller/js").Include
             (
               "~/Scripts/controller/DashBoardcontroller.js",
               "~/Scripts/controller/AdministrationController.js",
               "~/Scripts/controller/GroupManagementController.js",
               "~/Scripts/controller/UserManagementController.js",
               "~/Scripts/controller/AddUserPopupController.js",
               "~/Scripts/controller/AddFolderPopupController.js",
               "~/Scripts/controller/SearchController.js",
               "~/Scripts/controller/FolderController.js",
               "~/Scripts/controller/HeaderController.js",
               "~/Scripts/controller/UploadController.js",
               "~/Scripts/controller/DeleteController.js",
               "~/Scripts/controller/DeletePopupController.js",
               "~/Scripts/controller/TaskListController.js",
               "~/Scripts/controller/CreateWorkflowController.js",
               "~/Scripts/controller/ViewEditWFController.js",
               "~/Scripts/controller/WorkflowController.js",
               "~/Scripts/controller/WorkflowDetailController.js",               
               "~/Scripts/controller/WorkflowReportController.js",
               "~/Scripts/controller/ProcessDiagramController.js",
               "~/Scripts/controller/ToolPaneController.js",
               "~/Scripts/controller/MenuController.js",
               "~/Scripts/controller/ManagePermissionsPopupController.js",
                "~/Scripts/controller/MetaDataController.js",
               "~/Scripts/controller/AddGroupPopupController.js",
               "~/Scripts/controller/ManageGroupUsersPopupController.js"
             ));
        }
        private static void LoadDirective(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Directive/js").Include
             (
                "~/Scripts/Directive/FileDrop.js"              
             ));
        }
        private static void LoadFactory(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/factory/js").Include
             (
                "~/Scripts/factory/Interceptor.js",
                "~/Scripts/factory/Cache.js",
                "~/Scripts/factory/DataSharingService.js"
             ));
        }
        private static void LoadConstant(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/constants/js").Include
             (
               "~/Scripts/constants/Global.js"
             ));
        }
        private static void LoadBoostrap(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap/js").Include
             (
               "~/Scripts/bootstrap/bootstrap.min.js",
               "~/Scripts/bootstrap/ui-bootstrap-tpls-2.5.0.min.js"
             ));
        }
        private static void LoadAPI(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/httpfactory/js").Include
                 (
                   "~/Scripts/httpfactory/AuthenticationAPI.js",
                   "~/Scripts/httpfactory/FolderAPI.js",
                   "~/Scripts/httpfactory/AdministrationApi.js",
                   "~/Scripts/httpfactory/SearchAPI.js",
                   "~/Scripts/httpfactory/FileAPI.js",
                   "~/Scripts/httpfactory/WorkFlowAPI.js",
                   "~/Scripts/httpfactory/WorkflowReportApi.js"
                 ));
        }
        private static void LoadUnderscore(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/underscore/js").Include
                 (
                   "~/Scripts/underscore/underscore-min.js"                   
                 ));
        }
        private static void LoadService(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/service/js").Include
                 (
                   "~/Scripts/service/UserProfile.js"
                 ));
        }
        #endregion

        #region StyleSheets
        private static void LoadBootStarpStyle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css/bootstrap/css").Include
            (
              "~/css/bootstrap/bootstrap.min.css",
              "~/css/fontawesome/font-awesome.min.css"
            ));
        }
        private static void LoadBootGloabalStyle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css/css").Include
            (
              "~/css/global.css"
            ));
        }
        private static void LoadKendoStyle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css/kendo/css").Include
            (
              "~/css/kendo/kendo.common-material.min.css",
               "~/css/kendo/kendo.material.min.css",
                "~/css/kendo/kendo.material.mobile.min.css"
            ));
        }
        #endregion
    }
}
