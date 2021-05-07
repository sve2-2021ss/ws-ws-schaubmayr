using GraphQLService.DAL.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLService.DAL
{
    public class DbService : IDbService
    {
        public sve2_wsContext GetDbContext()
        {
            return new sve2_wsContext();
        }
    }
}
