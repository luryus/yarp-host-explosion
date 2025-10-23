using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);
const int routeCount = 300;
var routes = new List<RouteConfig>(routeCount);
var cluster = new ClusterConfig
{
    ClusterId = "cluster",
    Destinations = new Dictionary<string, DestinationConfig>
    {
        { "server", new DestinationConfig { Address = "http://127.0.0.1" } }
    }
};

for (var i = 1; i <= routeCount; i++)
{
    var hostName = $"route{i:D5}.example.com";
    var routeId = $"route_{i}";
    routes.Add(new RouteConfig
    {
        Order = -1000, RouteId = routeId, ClusterId = cluster.ClusterId,
        Match = new RouteMatch { Hosts = [hostName] },
    });
}

builder.Services.AddReverseProxy().LoadFromMemory(routes, [cluster]);
builder.Services.AddControllersWithViews();
builder.Services.AddHealthChecks();

var app = builder.Build();
app.UseRouting();
app.MapReverseProxy();
app.MapStaticAssets().RequireHost("localhost");
app.Run();