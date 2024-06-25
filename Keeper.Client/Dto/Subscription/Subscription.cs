using System;

namespace Keeper.Client;

public partial class Subscription : BaseDto
{
    public int Id { get; set; }

    public Information Organization { get; set; } = null!;

    public Information Plan { get; set; } = null!;
            
    public DateTimeOffset StartDate { get; set; }
            
    public DateTimeOffset EndDate { get; set; }

    public static Subscription Exec(int id, KeeperApiClient client)
    {
        var request = client.GetRequest($"api/subscription/get/{id}");

        return client.ExecuteWithHttp<Subscription>(request);
    }

    public static Subscription ExecTest(int id, KeeperApiClient client)
    {
        var request = client.GetRequest($"api/subscription/get/{id}");

        return client.ExecuteWithHttp<Subscription>(request);
    }
}