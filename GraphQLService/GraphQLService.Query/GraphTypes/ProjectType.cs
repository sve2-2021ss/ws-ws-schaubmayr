using GraphQL.Types;
using GraphQLService.Dto.Models;
using GraphQLService.Query.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLService.Query.GraphTypes
{
    public class ProjectType : ObjectGraphType<ProjectDto>
    {
        public ProjectType(IListRepository listRepository,
             ISeriesRepository seriesRepository)
        {
            Field(x => x.IdLab).Description("IdLab");
            Field(x => x.IdProject).Description("IdProject");
            Field(x => x.Name).Description("Name");
            Field(x => x.Customer).Description("Customer");
            Field(x => x.Timestamp, nullable: true).Description("Timestamp");
            Field<ListGraphType<ListType>>(
             "lists",
             resolve: x => listRepository.GetListsForProject(x.Source.IdLab,x.Source.IdProject)
             );
            Field<ListGraphType<SeriesType>>(
             "series",
             resolve: x => seriesRepository.GetSeriesForLab(x.Source.IdLab)
             );
        }
    }
}
