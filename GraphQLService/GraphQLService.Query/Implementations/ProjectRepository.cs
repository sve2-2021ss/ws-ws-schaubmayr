using GraphQLService.DAL;
using GraphQLService.Dto.Models;
using GraphQLService.Logic;
using GraphQLService.Query.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphQLService.Query.Implementations
{
    public class ProjectRepository : IProjectRepository
    {
        private IDbService _dbService;

        public ProjectRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<ProjectDto> GetAll()
        {
            return _dbService.GetDbContext().Projects.Convert();
        }

        public ProjectDto GetById(int idLab, int idProject)
        {
            return _dbService.GetDbContext().Projects
                .SingleOrDefault(e => e.IdLab == idLab && e.IdProject == idProject)?.Convert();
        }

        public IEnumerable<ProjectDto> GetProjectsForLab(int idLab)
        {
            return _dbService.GetDbContext().Projects.Where(e => e.IdLab == idLab).Convert();
        }
    }
}
