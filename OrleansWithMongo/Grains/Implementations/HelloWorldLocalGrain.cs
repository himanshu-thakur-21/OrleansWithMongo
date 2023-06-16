using OrleansWithMongo.Grains.Abstactions;

namespace OrleansWithMongo.Grains.Implementations
{
    public sealed class HelloWorldLocalGrain : Grain, IHelloWorldLocalGrain
    {
        private int _invocationCount;

        public Task<string> SayHelloToAsync(string name)
        {
            return Task.FromResult($"Hello {name} from {this.GetPrimaryKeyString()} - I've said hello {_invocationCount++} times.");
        }
    }
}
