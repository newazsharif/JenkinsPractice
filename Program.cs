using CommanderGQL;
using CommanderGQL.GraphQL;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.GraphQL.Platforms.Commands;
using GraphQL.Server.Ui.Voyager;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;
var provider = services?.BuildServiceProvider();
var configuration = provider?.GetRequiredService<IConfiguration>();
services?.AddPooledDbContextFactory<AppDbContext>(
                    opt => opt.UseSqlServer(configuration?.GetConnectionString("CommandConnStr"))
                    );
services?
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<PlatformType>()
    .AddType<CommandType>()
    .AddMutationType<Mututation>()
    .AddSubscriptionType<Subscription>()
    .AddFiltering()
    .AddSorting()
    .AddInMemorySubscriptions();

var app = builder.Build();
app.UseRouting();
app.UseWebSockets();
app.MapGraphQL();
app.UseGraphQLVoyager("/ui/graphql");

app.Run();
