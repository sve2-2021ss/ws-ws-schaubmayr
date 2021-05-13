using GraphQLService.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLService.Query.Interfaces
{
    public interface IProjectRepository
    {
        public IEnumerable<ProjectDto> GetAll();
        public IEnumerable<ProjectDto> GetProjectsForLab(int idLab);
    }
}
