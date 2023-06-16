using Orleans.Runtime;
using OrleansWithMongo.Grains.Abstactions;
using OrleansWithMongo.Grains.States;

namespace OrleansWithMongo.Grains.Implementations
{
    public sealed class HelloWorldDbGrain : IHelloWorldDbGrain
    {
        private readonly IPersistentState<HelloDbState> _helloDbState;

        public HelloWorldDbGrain([PersistentState(nameof(HelloDbState), "MongoDBStore")] IPersistentState<HelloDbState> helloDbState)
        {
            _helloDbState = helloDbState;
        }

        public async Task<string> SayHelloWorldAsync(string name)
        {
            var count = _helloDbState.State.InvocationCount++;
            await _helloDbState.WriteStateAsync();

            return $"Hello {name} from {nameof(HelloWorldDbGrain)} - I've said hello {count} times.";
        }
    }
}