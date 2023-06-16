namespace OrleansWithMongo.Grains.Abstactions
{
    public interface IHelloWorldDbGrain : IGrainWithGuidKey
    {
        Task<string> SayHelloWorldAsync(string name);
    }
}
