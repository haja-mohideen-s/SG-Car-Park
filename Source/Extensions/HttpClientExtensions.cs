public static class HttpClientExtensions
{
    public static string HdbClient = "HdbApiClient";
    public static string CarParkClient = "CarParkClient";
    public static void AddHdbHttpClients(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddHttpClient(HdbClient, option =>
        {
            option.BaseAddress = new Uri(configuration["HdbApiEndPoint"]);
        });

        serviceCollection.AddHttpClient(CarParkClient, option =>
        {
            option.BaseAddress = new Uri(configuration["CarParkEndPoint"]);
        });
    }
}