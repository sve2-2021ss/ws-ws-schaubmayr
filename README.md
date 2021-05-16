# GraphQL Project
## UseCase
The Main Goal is to get familiar with GraphQL and to know how much effort it is to use it in a "real" project.
* How to setup the project. 
* How to design the Api (Schema first or GraphType first). 
* How to create Queries and Mutations. 
* Data with multiple Keys and Relations

## Architecture
![Architecture](https://github.com/sve2-2021ss/ws-ws-schaubmayr/blob/master/doc/Architecture.PNG)
## Database
![Database](https://github.com/sve2-2021ss/ws-ws-schaubmayr/blob/master/doc/RD.PNG)
## Dto's
The Dto's have exactly the same properties like the entities from the Database, therefore they are not displayed here.
## Repositories
### Lab
```cs
 public interface ILabRepository
    {
        IEnumerable<LabDto> GetAll();
        LabDto GetById(int idLab);
        Task<LabDto> Create(LabDto lab);
        Task<LabDto> Update(LabDto lab, int idLab);
        Task<bool> Delete(int idLab);
    }
```
### Project
```cs
 public interface IProjectRepository
    {
        public IEnumerable<ProjectDto> GetAll();
        public IEnumerable<ProjectDto> GetProjectsForLab(int idLab);
        public ProjectDto GetById(int idLab, int idProject);
        public Task<ProjectDto> Create(ProjectDto project);
        public Task<ProjectDto> Update(ProjectDto project, int idLab, int idProject);
        public Task<bool> Delete(int idLab, int idProject);
    }
```
### Series
```cs
 public interface ISeriesRepository
    {
        public IEnumerable<SeriesDto> GetAll();
        public IEnumerable<SeriesDto> GetSeriesForProject(int idLab, int idProject);
        public IEnumerable<SeriesDto> GetSeriesForLab(int idLab);
        public SeriesDto GetById(int idLab, int idProject, int idSeries);
    }
```
### List
```cs
  public interface IListRepository
    {
        public IEnumerable<ListDto> GetAll();
        public IEnumerable<ListDto> GetListsForProject(int idLab, int idProject);
        public IEnumerable<ListDto> GetListsForLab(int idLab);
        public ListDto GetById(int idLab, int idProject, int idList);
    }
```
### Point
```cs
 public interface IPointRepository
    {
        public IEnumerable<PointDto> GetAll();
        public IEnumerable<PointDto> GetPointsForProject(int idLab, int idProject);
        public IEnumerable<PointDto> GetPointsForLab(int idLab);
        public IEnumerable<PointDto> GetPointsForList(int idLab, int idProject, int idList);
        public IEnumerable<PointDto> GetPointsForSeries(int idLab, int idProject, int idSeries);
        public PointDto GetById(int idLab, int idProject, int idSeries, int idPoint);
    }
```
## Approach
I used "GraphType first" because it feels way more "writing code" than "Schema first". Overall i prefer writing code then writing text schemas.
## ObjectGraphTypes
### LabType
```cs
  public class LabType : ObjectGraphType<LabDto>
    {
        public LabType(
            IProjectRepository projectRepository, 
            IListRepository listRepository, 
            ISeriesRepository seriesRepository,
            IPointRepository pointRepository)
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
            Field<ListGraphType<PointType>>(
             "points",
             resolve: x => pointRepository.GetPointsForLab(x.Source.IdLab)
             );
        }
    }
```
### ProjectType
```cs
 public class ProjectType : ObjectGraphType<ProjectDto>
    {
        public ProjectType(IListRepository listRepository,
             ISeriesRepository seriesRepository, IPointRepository pointRepository)
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
            Field<ListGraphType<PointType>>(
              "points",
              resolve: x => pointRepository.GetPointsForProject(x.Source.IdLab, x.Source.IdProject)
          );
        }
    }
```
### SeriesType
```cs
public class SeriesType : ObjectGraphType<SeriesDto>
    {
        public SeriesType(ISeriesRepository seriesRepository, IPointRepository pointRepository)
        {
            Field(x => x.IdLab).Description("IdLab");
            Field(x => x.IdProject).Description("IdProject");
            Field(x => x.IdSeries).Description("IdSeries");
            Field(x => x.Name).Description("Name");
            Field(x => x.Timestamp, nullable: true).Description("Timestamp");

            Field<ListGraphType<PointType>>(
              "points",
              resolve: x => pointRepository.GetPointsForSeries(x.Source.IdLab, x.Source.IdProject, x.Source.IdSeries)
          );
        }
    }
```
### ListType
```cs
 public class ListType : ObjectGraphType<ListDto>
    {
        public ListType(IListRepository listRepository, IPointRepository pointRepository)
        {
            Field(x => x.IdLab).Description("IdLab");
            Field(x => x.IdProject).Description("IdProject");
            Field(x => x.IdList).Description("IdList");
            Field(x => x.Name).Description("Name");
            Field(x => x.Timestamp, nullable: true).Description("Timestamp");

            Field<ListGraphType<PointType>>(
               "points",
               resolve: x => pointRepository.GetPointsForList(x.Source.IdLab, x.Source.IdProject, x.Source.IdList)
           );
        }
    }
```
### PointType
```cs
 public class PointType : ObjectGraphType<PointDto>
    {
        public PointType(IPointRepository pointRepository)
        {
            Field(x => x.IdLab).Description("IdLab");
            Field(x => x.IdProject).Description("IdProject");
            Field(x => x.IdSeries).Description("IdSeries");
            Field(x => x.IdPoint).Description("IdPoint");
            Field(x => x.IdList, nullable:true).Description("IdList");
            Field(x => x.Name).Description("Name");
            Field(x => x.Timestamp, nullable: true).Description("Timestamp");
        }
    }
```
## InputObjectGraphTypes
I chose to create Mutations for Project and Lab only, since it's the same for all other entities.
### LabInputType
```cs
public class LabInputType : InputObjectGraphType
    {
        public LabInputType()
        {
            Name = "LabInput";
            Field<NonNullGraphType<StringGraphType>>(nameof(LabDto.Name));
            Field<NonNullGraphType<StringGraphType>>(nameof(LabDto.Location));
        }
    }
```
### ProjectInputType
```cs
 public class ProjectInputType : InputObjectGraphType
    {
        public ProjectInputType()
        {
            Name = "ProjectInput";
            Field<NonNullGraphType<IntGraphType>>(nameof(ProjectDto.IdLab));
            Field<NonNullGraphType<StringGraphType>>(nameof(ProjectDto.Name));
            Field<NonNullGraphType<StringGraphType>>(nameof(ProjectDto.Customer));
        }
    }
```
## AppQuery
```cs
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(
            ILabRepository labRepository,
            IProjectRepository projectRepository,
            IListRepository listRepository,
             ISeriesRepository seriesRepository,
             IPointRepository pointRepository)
        {
            #region GetAll
            Field<ListGraphType<LabType>>(
                "labs",
                resolve: context => labRepository.GetAll()
                );

            Field<ListGraphType<ProjectType>>(
                "projects",
                resolve: context => projectRepository.GetAll()
                );

            Field<ListGraphType<ListType>>(
               "lists",
               resolve: context => listRepository.GetAll()
               );

            Field<ListGraphType<SeriesType>>(
                "series",
                resolve: context => seriesRepository.GetAll()
            );

            Field<ListGraphType<PointType>>(
              "points",
              resolve: x => pointRepository.GetAll()
          );
            #endregion
            #region GetById
            Field<LabType>(
              "lab",
              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name="idLab" }),
              resolve: context =>
              {
                  return labRepository.GetById(context.GetArgument<int>("idLab"));
              }
              );

            Field<ProjectType>(
             "project",
               arguments: new QueryArguments(
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idLab" },
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idProject" }
                 ),
             resolve: context =>
             {
                 return projectRepository.GetById(
                   context.GetArgument<int>("idLab"),
                   context.GetArgument<int>("idProject"));
             }
             );

            Field<SeriesType>(
             "serie",
               arguments: new QueryArguments(
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idLab" },
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idProject" },
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idSeries" }
                 ),
             resolve: context =>
             {
                 return seriesRepository.GetById(
                   context.GetArgument<int>("idLab"),
                   context.GetArgument<int>("idProject"),
                   context.GetArgument<int>("idSeries"));
             }
             );

            Field<ListType>(
             "list",
               arguments: new QueryArguments(
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idLab" },
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idProject" },
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idList" }
                 ),
             resolve: context =>
             {
                 return listRepository.GetById(
                   context.GetArgument<int>("idLab"),
                   context.GetArgument<int>("idProject"),
                   context.GetArgument<int>("idList"));
             }
             );

            Field<PointType>(
             "point",
             arguments: new QueryArguments(
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idLab" },
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idProject" },
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idSeries" },
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idPoint" }
                 ),
             resolve: context =>
             {
                 return pointRepository.GetById(
                     context.GetArgument<int>("idLab"),
                     context.GetArgument<int>("idProject"),
                     context.GetArgument<int>("idSeries"),
                     context.GetArgument<int>("idPoint"));
             }
             );
            #endregion
        }
    }
```
## AppMutations
```cs
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
```
## AppSchema
##### Code
```cs
 public class AppSchema : Schema
    {
        public AppSchema(IServiceProvider provider) : base(provider)
        {
            Query = (IObjectGraphType)provider.GetRequiredService(typeof(AppQuery));
            Mutation = (IObjectGraphType)provider.GetRequiredService(typeof(AppMutation));
        }
    }
```
##### Schema displayed in GraphQL Playground
```javascript
schema {
  query: AppQuery
  mutation: AppMutation
}

type AppQuery {
  labs: [LabType]
  projects: [ProjectType]
  lists: [ListType]
  series: [SeriesType]
  points: [PointType]
  lab(idLab: ID!): LabType
  project(idLab: ID!, idProject: ID!): ProjectType
  serie(idLab: ID!, idProject: ID!, idSeries: ID!): SeriesType
  list(idLab: ID!, idProject: ID!, idList: ID!): ListType
  point(idLab: ID!, idProject: ID!, idSeries: ID!, idPoint: ID!): PointType
}

type LabType {
  # IdLab
  idLab: Int!

  # Name
  name: String!

  # Location
  location: String!

  # Timestamp
  timestamp: DateTime
  projects: [ProjectType]
  lists: [ListType]
  series: [SeriesType]
  points: [PointType]
}

# The `DateTime` scalar type represents a date and time. `DateTime` expects timestamps to be formatted in accordance with the [ISO-8601](https://en.wikipedia.org/wiki/ISO_8601) standard.
scalar DateTime

type ProjectType {
  # IdLab
  idLab: Int!

  # IdProject
  idProject: Int!

  # Name
  name: String!

  # Customer
  customer: String!

  # Timestamp
  timestamp: DateTime
  lists: [ListType]
  series: [SeriesType]
  points: [PointType]
}

type ListType {
  # IdLab
  idLab: Int!

  # IdProject
  idProject: Int!

  # IdList
  idList: Int!

  # Name
  name: String!

  # Timestamp
  timestamp: DateTime
  points: [PointType]
}

type PointType {
  # IdLab
  idLab: Int!

  # IdProject
  idProject: Int!

  # IdSeries
  idSeries: Int!

  # IdPoint
  idPoint: Int!

  # IdList
  idList: Int

  # Name
  name: String!

  # Timestamp
  timestamp: DateTime
}

type SeriesType {
  # IdLab
  idLab: Int!

  # IdProject
  idProject: Int!

  # IdSeries
  idSeries: Int!

  # Name
  name: String!

  # Timestamp
  timestamp: DateTime
  points: [PointType]
}

type AppMutation {
  createLab(lab: LabInput!): LabType
  updateLab(lab: LabInput!, idLab: ID!): LabType
  deleteLab(idLab: ID!): String
  createProject(project: ProjectInput!): ProjectType
  updateProject(project: ProjectInput!, idLab: ID!, idProject: ID!): ProjectType
  deleteProject(idLab: ID!, idProject: ID!): String
}

input LabInput {
  name: String!
  location: String!
}

input ProjectInput {
  idLab: Int!
  name: String!
  customer: String!
}

```
## Tests
### Queries
#### Nested Query - All Tables
##### Request
```javascript
query Labs {
  lab(idLab:1) {
    idLab
    name
    location
    projects {
      idProject
      name
      customer
      lists {
        idList
        name
      }
      series {
        idSeries
        name
        points {
          idPoint
          name
        }
      }
    }
  }
}
```
##### Response
```javascript
{
  "data": {
    "lab": {
      "idLab": 1,
      "name": "Lab 1",
      "location": "Linz",
      "projects": [
        {
          "idProject": 1,
          "name": "L Project 1",
          "customer": "A",
          "lists": [
            {
              "idList": 1,
              "name": "List 1 A"
            },
            {
              "idList": 2,
              "name": "List 2 A"
            }
          ],
          "series": [
            {
              "idSeries": 1,
              "name": "Series 1",
              "points": [
                {
                  "idPoint": 1,
                  "name": "P1"
                },
                {
                  "idPoint": 2,
                  "name": "P2"
                }
              ]
            },
            {
              "idSeries": 2,
              "name": "Series 2",
              "points": [
                {
                  "idPoint": 1,
                  "name": "P3"
                },
                {
                  "idPoint": 2,
                  "name": "P4"
                }
              ]
            },
            {
              "idSeries": 1,
              "name": "Series 3",
              "points": []
            },
            {
              "idSeries": 2,
              "name": "Series 4",
              "points": []
            }
          ]
        },
        {
          "idProject": 2,
          "name": "W Project 1",
          "customer": "C",
          "lists": [
            {
              "idList": 1,
              "name": "List 1 B"
            },
            {
              "idList": 2,
              "name": "List 2 B"
            }
          ],
          "series": [
            {
              "idSeries": 1,
              "name": "Series 1",
              "points": [
                {
                  "idPoint": 1,
                  "name": "P1"
                },
                {
                  "idPoint": 2,
                  "name": "P2"
                }
              ]
            },
            {
              "idSeries": 2,
              "name": "Series 2",
              "points": [
                {
                  "idPoint": 1,
                  "name": "P3"
                },
                {
                  "idPoint": 2,
                  "name": "P4"
                }
              ]
            },
            {
              "idSeries": 1,
              "name": "Series 3",
              "points": []
            },
            {
              "idSeries": 2,
              "name": "Series 4",
              "points": []
            }
          ]
        }
      ]
    }
  },
  "extensions": {}
}
```
### Mutations
#### CreateLab
##### Request
```javascript
mutation CreateLab {
  createLab(lab: { name: "HomeLab", location: "Home" }) {
    idLab
    name
    location
  }
}
```
##### Response
```javascript
{
  "data": {
    "createLab": {
      "idLab": 4,
      "name": "HomeLab",
      "location": "Home"
    }
  },
  "extensions": {}
}
```
#### UpdateLab
##### Request
```javascript
mutation UpdateLab {
  updateLab(idLab: 3, lab: { name: "SuperLab", location: "Everywhere" }) {
    idLab
    name
    location
  }
}
```
##### Response
```javascript
{
  "data": {
    "updateLab": {
      "idLab": 3,
      "name": "SuperLab",
      "location": "Everywhere"
    }
  },
  "extensions": {}
}
```
#### DeleteLab
```javascript
{
  "data": {
    "deleteLab": "Entity deleted successfully"
  },
  "extensions": {}
}
```
##### Request
```javascript
mutation DeleteLab{
  deleteLab(idLab:3)
}
```
##### Response
```javascript
```
#### CreateProject
##### Request
```javascript
mutation CreateProject {
  createProject(
    project: { idLab: 4, name: "GraphQLInsertProject", customer: "GraphQL" }
  ) {
    idLab
    idProject
    name
    customer
  }
}
```
##### Response
```javascript
{
  "data": {
    "createProject": {
      "idLab": 4,
      "idProject": 1,
      "name": "GraphQLInsertProject",
      "customer": "GraphQL"
    }
  },
  "extensions": {}
}
```
#### UpdateProject
##### Request
```javascript
mutation UpdateProject {
  updateProject(
    idLab: 4
    idProject: 1
    project: {
      idLab: 4
      name: "GraphQLInsertProject-Second"
      customer: "Everything"
    }
  ) {
    idLab
    idProject
    name
    customer
  }
}
```
##### Response
```javascript
{
  "data": {
    "updateProject": {
      "idLab": 4,
      "idProject": 1,
      "name": "GraphQLInsertProject-Second",
      "customer": "Everything"
    }
  },
  "extensions": {}
}
```
#### DeleteProject
##### Request
```javascript
mutation DeleteProject {
  deleteProject(idLab: 4, idProject: 1)
}
```
##### Response
```javascript
{
  "data": {
    "deleteProject": "Entity deleted successfully"
  },
  "extensions": {}
}
```
## Conclusio
On the first look it seems quite complicated when you are only familiar with REST-Api's, but works out pretty well. It's a very powerful technology. If there are many Endpoints to setup in an Rest-Api, i'll quess using GraphQL will be easier, since the client defines the queries itself.

GraphQl works really good with multi-nested Entities.

Next things i would try are Dataloader and AzureAd Authentication
