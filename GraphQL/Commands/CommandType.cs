namespace CommanderGQL.GraphQL.Platforms.Commands;

public class CommandType : ObjectType<Command>{
    protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
    {
        descriptor.Description("Command information");
        descriptor
            .Field(x=>x.Platform)
            .UseDbContext<AppDbContext>()
            .ResolveWith<Resolvers>(x => x.GetPlatform(default!,default!));
    }

    public class Resolvers{
        public Platform? GetPlatform([Parent]Command command,[ScopedService] AppDbContext context){
            return context.Platforms?.FirstOrDefault(x=>x.Id.Equals(command.PlatFormId));
        }
    }
}