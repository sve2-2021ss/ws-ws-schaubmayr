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
    public class ListRepository : IListRepository
    {
        private IDbService _dbService;

        public ListRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<ListDto> GetAll()
        {
            return _dbService.GetDbContext().Lists.Convert();
        }

        public ListDto GetById(int idLab, int idProject, int idList)
        {
            return _dbService.GetDbContext().Lists
                .SingleOrDefault(e => e.IdLab == idLab
                                && e.IdProject == idProject
                                && e.IdList == idList)?.Convert();
        }

        public IEnumerable<ListDto> GetListsForLab(int idLab)
        {
            return _dbService.GetDbContext().Lists.Where(e => e.IdLab == idLab).Convert();
        }

        public IEnumerable<ListDto> GetListsForProject(int idLab, int idProject)
        {
            return _dbService.GetDbContext().Lists
                .Where(e => e.IdLab == idLab && e.IdProject == idProject)
                .Convert();
        }
    }
}
