using GraphQL.Types;
using Bingo.Models;
using Bingo.Repositories;

namespace  Bingo.GraphQL.Types 
{
    class UserType : ObjectGraphType<User>
    {
        public UserType(UserRepository userRepository)
        {
            Name = "User";
            Field(x => x.Id);
            Field(x => x.Name).Description("User name");
            Field(x => x.Email).Description("User email");
            Field(x => x.Password).Description("User password");
        }
    }
}