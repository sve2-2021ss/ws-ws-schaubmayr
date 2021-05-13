﻿using GraphQL.Types;
using GraphQLService.Dto.Models;
using GraphQLService.Query.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLService.Query.GraphTypes
{
    public class ListType : ObjectGraphType<ListDto>
    {
        public ListType(IListRepository listRepository)
        {
            Field(x => x.IdLab).Description("IdLab");
            Field(x => x.IdProject).Description("IdProject");
            Field(x => x.IdList).Description("IdList");
            Field(x => x.Name).Description("Name");
            Field(x => x.Timestamp, nullable: true).Description("Timestamp");
        }
    }
}
