using System.Web;
using System.Web.Optimization;

namespace FootballPredictor
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap/css").Include(
                      "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Content/Authenticated/Models/js").Include(
                    "~/Scripts/Models/Applications/Application.js",
                      "~/Scripts/Models/Fixtures/Fixture.js",
                      "~/Scripts/Models/Fixtures/FixtureScore.js",
                      "~/Scripts/Models/Competitions/Competitions.js",
                      "~/Scripts/Models/Seasons/Season.js",
                      "~/Scripts/Models/CompetitionSeason/Season.js",
                      "~/Scripts/Models/CompetitionSeason/CompetitionSeason.js",
                      "~/Scripts/Models/CompetitionSeason/CompetitionSeasonFixture.js"
                      )
            );

            bundles.Add(new ScriptBundle("~/Content/Authenticated/js").Include(
                      "~/Scripts/Authenticated/Authenticated.js"));
        }
    }
}
