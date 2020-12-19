using GraphQL;
using GraphQL.Types;

namespace  Bingo.GraphQL 
{
    class BingoSchema : Schema
    {
        public BingoSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<BingoQuery>();
            Mutation = resolver.Resolve<BingoMutation>(); 
            Subscription=resolver.Resolve<BingoSuscription>();
        }
    }
}