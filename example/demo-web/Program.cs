using Microsoft.AspNetCore.Http.Extensions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/{**catchall}", (HttpContext httpContext) => $"Current URL: {httpContext.Request.GetDisplayUrl()}");

app.Run();
