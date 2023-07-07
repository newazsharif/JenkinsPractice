namespace CommanderGQL.GraphQL.Platforms;


public class PlatformType : ObjectType<Platform>{
    protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
    {
        descriptor.Description("Defines the software platforms");
        descriptor.Field(x=>x.LicenseKey).Ignore();
        descriptor.Field(x=>x.Commands)
                .ResolveWith<Resolvers>(p=>p.GetCommands(default!,default!))
                .UseDbContext<AppDbContext>()
                .Description("Get Commands by platform");
    }

    public class Resolvers{
        public IQueryable<Command> GetCommands([Parent]Platform platform, [ScopedService] AppDbContext context){
            return context.Commands.Where(p=>p.PlatFormId == platform.Id);
        }
    }
}