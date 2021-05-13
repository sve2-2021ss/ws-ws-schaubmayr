using GraphQLService.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLService.Query.Interfaces
{
    public interface IListRepository
    {
        public IEnumerable<ListDto> GetAll();
        public IEnumerable<ListDto> GetListsForProject(int idLab, int idProject);
        public IEnumerable<ListDto> GetListsForLab(int idLab);
    }
}
