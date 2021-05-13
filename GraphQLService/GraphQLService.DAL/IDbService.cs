using GraphQLService.DAL;
using GraphQLService.DAL.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLService.DAL
{
    public interface IDbService
    {
        public sve2_wsContext GetDbContext();
    }
}
