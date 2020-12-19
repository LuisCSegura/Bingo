using GraphQL.Types;
using Bingo.Models;
using Bingo.Repositories;

namespace  Bingo.GraphQL.Types 
{
    class AuthResponseType : ObjectGraphType<AuthResponse>
    {
        public AuthResponseType()
        {
            Name = "AuthResponse";
            Field(x => x.Id).Description("The signed in user id");
            Field(x => x.UserName).Description("The signed in user name");
            Field(x => x.Token).Description("The authentication token");
        }
    }
}