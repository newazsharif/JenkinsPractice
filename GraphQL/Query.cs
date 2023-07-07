using HotChocolate;

namespace CommanderGQL.GraphQL;


public class Query{
    [UseDbContext(typeof(AppDbContext))]
    //  [UseProjection]
     [UseSorting]
     [UseFiltering]
    public IQueryable<Platform> GetPlatforms([ScopedService] AppDbContext context){
        return context.Platforms;
    }
     [UseDbContext(typeof(AppDbContext))]
    public Platform GetPlatform([ScopedService] AppDbContext context,int id){
        return context.Platforms.FirstOrDefault(x=>x.Id.Equals(id));
    }
    [UseDbContext(typeof(AppDbContext))]
    [UseFiltering]
    [UseSorting]
     public IQueryable<Command> GetCommands([ScopedService] AppDbContext context){
        return context.Commands;
     }
}