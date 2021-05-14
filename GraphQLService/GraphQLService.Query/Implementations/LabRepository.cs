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
    public class LabRepository : ILabRepository
    {
        private IDbService _dbService;

        public LabRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<LabDto> GetAll()
        {
            return _dbService.GetDbContext().Labs.Convert();
        }

        public LabDto GetById(int idLab)
        {
            return _dbService.GetDbContext().Labs.SingleOrDefault(e => e.IdLab == idLab)?.Convert();
        }
    }
}
