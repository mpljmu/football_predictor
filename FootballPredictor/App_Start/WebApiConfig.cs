using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FootballPredictor
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "CompetitionSeason",
                routeTemplate: "api/competitionseason/{competitionSeasonId}/{controller}",
                defaults: new { }
            );

            config.Routes.MapHttpRoute(
                name: "CompetitionSeasonPlayer",
                routeTemplate: "api/competitionseason/{competitionSeasonId}/player/{playerId}/{controller}",
                defaults: new { }
            );

            config.Routes.MapHttpRoute(
                name: "User",
                routeTemplate: "api/users/{userId}/{controller}/{resourceId}",
                defaults: new { resourceId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Competition",
                routeTemplate: "api/competitions/{competitionId}/{controller}/{resourceId}",
                defaults: new { resourceId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
