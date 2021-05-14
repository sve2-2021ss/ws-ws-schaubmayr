using GraphQL;
using GraphQL.Types;
using GraphQLService.Dto.Models;
using GraphQLService.Query.GraphTypes;
using GraphQLService.Query.GraphTypes.InputTypes;
using GraphQLService.Query.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLService.Query
{
    public class AppMutation : ObjectGraphType
    {
        public AppMutation(
           ILabRepository labRepository,
           IProjectRepository projectRepository,
           IListRepository listRepository,
           ISeriesRepository seriesRepository,
           IPointRepository pointRepository)
        {
            #region LabMutations
            FieldAsync<LabType>(
                "createLab",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<LabInputType>> { Name = "lab" }),
                resolve: async context => await labRepository.Create(context.GetArgument<LabDto>("lab"))
                );

            FieldAsync<LabType>(
                "updateLab",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LabInputType>> { Name = "lab"},
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idLab" }
                    ),
                resolve: async context =>
                {
                    var lab = context.GetArgument<LabDto>("lab");
                    var labId = context.GetArgument<int>("idLab");

                    if (labRepository.GetById(labId) == null)
                    {
                        context.Errors.Add(new ExecutionError("Entity does not exist!"));
                        return null;
                    }

                    return await labRepository.Update(lab, labId);

                });

            FieldAsync<StringGraphType>(
                "deleteLab",
                 arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idLab" }
                    ),
                resolve: async context =>
                {
                    var labId = context.GetArgument<int>("idLab");

                    if (labRepository.GetById(labId) == null)
                    {
                        context.Errors.Add(new ExecutionError("Entity does not exist!"));
                        return null;
                    }

                    if (await labRepository.Delete(labId))
                        return "Entity deleted successfully";
                    else
                        return "Removing failed!";

                });
            #endregion
            #region ProjectMutations
            FieldAsync<ProjectType>(
               "createProject",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProjectInputType>> { Name = "project" }),
               resolve: async context => await projectRepository.Create(context.GetArgument<ProjectDto>("project"))
               );

            FieldAsync<ProjectType>(
                "updateProject",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProjectInputType>> { Name = "project" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idLab" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idProject" }
                    ),
                resolve: async context =>
                {
                    var project = context.GetArgument<ProjectDto>("project");
                    var labId = context.GetArgument<int>("idLab");
                    var projectId = context.GetArgument<int>("idProject");

                    if (projectRepository.GetById(labId, projectId) == null)
                    {
                        context.Errors.Add(new ExecutionError("Entity does not exist!"));
                        return null;
                    }

                    return await projectRepository.Update(project, labId, projectId);

                });

            FieldAsync<StringGraphType>(
                "deleteProject",
                 arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idLab" },
                     new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idProject" }
                    ),
                resolve: async context =>
                {
                    var labId = context.GetArgument<int>("idLab");
                    var projectId = context.GetArgument<int>("idProject");

                    if (projectRepository.GetById(labId, projectId) == null)
                    {
                        context.Errors.Add(new ExecutionError("Entity does not exist!"));
                        return null;
                    }

                    if (await projectRepository.Delete(labId, projectId))
                        return "Entity deleted successfully";
                    else
                        return "Removing failed!";

                });
            #endregion
        }
    }
}
