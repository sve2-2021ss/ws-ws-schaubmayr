using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLService.Query
{
    public class AppSchema : Schema
    {
        public AppSchema(IServiceProvider provider) : base(provider)
        {
            Query = (IObjectGraphType)provider.GetRequiredService(typeof(AppQuery));
            Mutation = (IObjectGraphType)provider.GetRequiredService(typeof(AppMutation));
        }
    }
}
