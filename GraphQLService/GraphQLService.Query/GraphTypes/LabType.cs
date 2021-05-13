using GraphQL.Types;
using GraphQLService.Dto.Models;
using GraphQLService.Query.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLService.Query.GraphTypes
{
    public class LabType : ObjectGraphType<LabDto>
    {
        public LabType(
            IProjectRepository projectRepository, 
            IListRepository listRepository, 
            ISeriesRepository seriesRepository)
        {
            Field(x => x.IdLab).Description("IdLab");
            Field(x => x.Name).Description("Name");
            Field(x => x.Location).Description("Location");
            Field(x => x.Timestamp,nullable:true).Description("Timestamp");
            Field<ListGraphType<ProjectType>>(
                "projects",
                resolve: x => projectRepository.GetProjectsForLab(x.Source.IdLab)
                );
            Field<ListGraphType<ListType>>(
               "lists",
               resolve: x => listRepository.GetListsForLab(x.Source.IdLab)
               );
            Field<ListGraphType<SeriesType>>(
              "series",
              resolve: x => seriesRepository.GetSeriesForLab(x.Source.IdLab)
              );
        }
    }
}
