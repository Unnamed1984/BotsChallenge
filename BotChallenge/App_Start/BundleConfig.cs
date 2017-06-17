using System.Web;
using System.Web.Optimization;

namespace BotChallenge
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/lib/jquery-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/lib/bootstrap.js",
                      "~/Scripts/lib/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/register").Include("~/Scripts/register.js"));
            bundles.Add(new ScriptBundle("~/bundles/waiting").Include("~/Scripts/waiting.js"));

            bundles.Add(new ScriptBundle("~/bundles/SignalRSrcipts").Include("~/Scripts/lib/jquery-1.6.4.min.js")
                                                                    .Include("~/Scripts/lib/jquery.signalR-2.2.1.min.js")
                                                                    .Include("~/signalr/hubs")
                                                                    .Include("~/Scripts/signalRPart.js"));

            bundles.Add(new ScriptBundle("~/bundles/phaser-lib").Include("~/Scripts/lib/phaser.js"));

            bundles.Add(new ScriptBundle("~/bundles/phaser-core").Include("~/Scripts/core/Bot.js")
                                                                    .Include("~/Scripts/core/BotsController.js")
                                                                    .Include("~/Scripts/core/phaser.game.js"));

            bundles.Add(new ScriptBundle("~/bundles/phaser-modules").Include("~/Scripts/modules/bots.module.js")
                                                                        .Include("~/Scripts/modules/camera.module.js")
                                                                        .Include("~/Scripts/module/initializing.module.js")
                                                                        .Include("~/Scripts/modules/util.js")
                                                                        .Include("~/Scripts/modules/ajax.module.js")
                                                                        .Include("~/Scripts/modules/compilation.module.js")
                                                                        .Include("~/Scripts/modules/run.module.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalRGame").Include("~/Scripts/jquery.signalR-2.2.1.min.js")
                                                                    .Include("~/signalr/hubs")
                                                                    .Include("~/Scripts/signalRGame.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Site.css",
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/game").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/game.css"));
        }
    }
}
