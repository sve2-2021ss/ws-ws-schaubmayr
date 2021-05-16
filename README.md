# GraphQL Project
## UseCase
## Architecture
![Architecture](https://github.com/sve2-2021ss/ws-ws-schaubmayr/blob/master/doc/Architecture.PNG)
## Database
![Database](https://github.com/sve2-2021ss/ws-ws-schaubmayr/blob/master/doc/RD.PNG)
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
### LabInputType
```cs
```
### ProjectInputType
```cs
```
## AppQuery
```cs
```
## AppMutations
```cs
```
## AppSchema
##### Code
```cs
```
##### Schema displayed in GraphQL Playground
```cs
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
