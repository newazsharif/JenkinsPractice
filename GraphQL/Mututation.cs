using HotChocolate.Subscriptions;

namespace CommanderGQL.GraphQL;

public class Mututation{
    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddPlatformPayload> AddPlatform(
                AddPlatformInput input,
                [ScopedService] AppDbContext context,
                [Service] ITopicEventSender sender,
                CancellationToken cancellationToken
                )
        {
        Platform platform = new()
        {
            Name = input.Name
        };
        await context.AddAsync(platform);
        AddPlatformPayload payload = new(
            platform
        );
        await context.SaveChangesAsync();
        await sender.SendAsync(nameof(Subscription.OnPlatformAdded),platform,cancellationToken);
        return payload;
    }
}