using HomeInventoryManager.InventoryManager.Models;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace HomeInventoryManager.InventoryManager.GraphQL;

public class Subscription
{
    [SubscribeAndResolve]
    public async ValueTask<ISourceStream<Product>> OnProductCreate([Service] ITopicEventReceiver eventReceiver, CancellationToken cancellationToken)
        => await eventReceiver.SubscribeAsync<string, Product>("ProductCreated", cancellationToken);
}