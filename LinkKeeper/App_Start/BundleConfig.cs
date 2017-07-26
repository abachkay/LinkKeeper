using System.Web.Optimization;

namespace LinkKeeper.API
{
    public class BundleConfig
    {        
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/appScripts").Include(
                        "~/Scripts/app/linkKeeper.module.js",
                        "~/Scripts/app/index.service.js",
                        "~/Scripts/app/index.controller.js",
                        "~/Scripts/app/pages/welcome/welcome.controller.js",
                        "~/Scripts/app/pages/register/register.service.js",
                        "~/Scripts/app/pages/register/register.controller.js",
                        "~/Scripts/app/pages/login/login.service.js",
                        "~/Scripts/app/pages/login/login.controller.js",
                        "~/Scripts/app/pages/links/links.service.js",
                        "~/Scripts/app/pages/links/links.controller.js",
                        "~/Scripts/app/linkKeeper.config.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/libs/angular/angular.min.js",
                        "~/Scripts/libs/angular/angular-cookies.min.js",
                        "~/Scripts/libs/angular/angular-route.min.js",
                        "~/Scripts/libs/angular/angular-ui-router.min.js"));   

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/libs/jquery/jquery-3.1.1.min.js"));   
            
            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
                      "~/Scripts/libs/materialize/materialize.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/materialize/css/materialize.min.css",
                      "~/Content/Site.css"));
        }
    }
}
