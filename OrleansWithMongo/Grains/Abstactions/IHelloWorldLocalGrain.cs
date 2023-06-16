namespace OrleansWithMongo.Grains.Abstactions
{
    public interface IHelloWorldLocalGrain : IGrainWithStringKey
    {
        Task<string> SayHelloToAsync(string name);
    }
}
