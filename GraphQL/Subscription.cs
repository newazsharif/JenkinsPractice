namespace CommanderGQL.GraphQL;


public class Subscription{
    [Subscribe]
    [Topic]
    public async Task<Platform> OnPlatformAdded([EventMessage] Platform platform){
        await Task.CompletedTask;
        return platform;
    }
}