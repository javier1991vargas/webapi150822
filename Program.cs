using WebApAutores;
var builder = WebApplication.CreateBuilder(args);

var StarUp = new StarUp(builder.Configuration);

StarUp.ConfigureServices(builder.Services);
var app = builder.Build();
StarUp.Configure(app,app.Environment);

app.Run();
