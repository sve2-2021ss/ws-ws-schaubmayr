using GraphQLService.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLService.Query.Interfaces
{
    public interface IProjectRepository
    {
        public IEnumerable<ProjectDto> GetAll();
        public IEnumerable<ProjectDto> GetProjectsForLab(int idLab);
        public ProjectDto GetById(int idLab, int idProject);
        public Task<ProjectDto> Create(ProjectDto project);
        public Task<ProjectDto> Update(ProjectDto project, int idLab, int idProject);
        public Task<bool> Delete(int idLab, int idProject);
    }
}
