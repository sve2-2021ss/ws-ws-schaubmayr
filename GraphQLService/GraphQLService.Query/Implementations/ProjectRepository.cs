using GraphQLService.DAL;
using GraphQLService.Dto.Models;
using GraphQLService.Logic;
using GraphQLService.Query.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLService.Query.Implementations
{
    public class ProjectRepository : IProjectRepository
    {
        private IDbService _dbService;

        public ProjectRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<ProjectDto> Create(ProjectDto project)
        {
            var context = _dbService.GetDbContext();
            project.IdProject = 
                context.Projects.Any(e=>e.IdLab == project.IdLab) 
                ? context.Projects.Where(e=>e.IdLab == project.IdLab).Max(e => e.IdProject) + 1
                : 1;

            project.Timestamp = DateTime.UtcNow;
            var entity = (await context.Projects.AddAsync(project.Convert())).Entity.Convert();
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int idLab, int idProject)
        {
            var context = _dbService.GetDbContext();
            var entity = GetById(idLab,idProject);
            context.Remove(entity.Convert());
            return await context.SaveChangesAsync() == 1;
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

        public async Task<ProjectDto> Update(ProjectDto project, int idLab, int idProject)
        {
            var context = _dbService.GetDbContext();

            var entity = GetById(idLab,idProject);
            entity.Name = project.Name;
            entity.Customer = project.Customer;


            entity = context.Update(entity.Convert()).Entity.Convert();
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
