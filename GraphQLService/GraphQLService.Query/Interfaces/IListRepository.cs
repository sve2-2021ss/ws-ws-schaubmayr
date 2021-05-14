using GraphQLService.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLService.Query.Interfaces
{
    public interface IListRepository
    {
        public IEnumerable<ListDto> GetAll();
        public IEnumerable<ListDto> GetListsForProject(int idLab, int idProject);
        public IEnumerable<ListDto> GetListsForLab(int idLab);
        public ListDto GetById(int idLab, int idProject, int idList);
        public Task<ListDto> Create(ListDto list);
    }
}
