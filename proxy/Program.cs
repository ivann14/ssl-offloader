using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

var proxyToUrl = builder.Configuration.GetValue<string>("proxyToUrl");
Console.WriteLine($"Passed value: {proxyToUrl}");
if (proxyToUrl is null)
{
    Console.WriteLine($"Parameter 'proxyToUrl' with value '{proxyToUrl?.ToString()}' to where proxy requests without SSL" +
        " was either empty or not valid URL. Default URL 'http://localhost:8080' is being used.");
}
var url = proxyToUrl ?? "http://localhost:8080";

var routeConfig = new RouteConfig
{
    RouteId = "routeId",
    ClusterId = "clusterId",
    Match = new RouteMatch
    {
        Path = "/{**catch-all}"
    },

}
.WithTransformCopyRequestHeaders(copy: true)
.WithTransformUseOriginalHostHeader(useOriginal: true);


var clusterConfig = new ClusterConfig
{
    ClusterId = "clusterId",
    Destinations = new Dictionary<string, DestinationConfig>
    {
        { "webapp", new DestinationConfig { Address = url} },
    }
};

builder.Services.AddReverseProxy().LoadFromMemory([routeConfig], [clusterConfig]);

var app = builder.Build();
app.MapReverseProxy();
app.Run();
