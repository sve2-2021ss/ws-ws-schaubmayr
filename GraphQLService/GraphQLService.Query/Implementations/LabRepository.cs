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
    public class LabRepository : ILabRepository
    {
        private IDbService _dbService;

        public LabRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<LabDto> Create(LabDto lab)
        {
            var context = _dbService.GetDbContext();
            lab.IdLab = context.Labs.Max(e => e.IdLab)+1;
            lab.Timestamp = DateTime.UtcNow;
            var entity = (await context.Labs.AddAsync(lab.Convert())).Entity.Convert();
            await context.SaveChangesAsync();
            return entity;

        }

        public async Task<bool> Delete(int idLab)
        {
            var context = _dbService.GetDbContext();
            var entity = GetById(idLab);
            context.Remove(entity.Convert());
            return await context.SaveChangesAsync() == 1;
        }

        public IEnumerable<LabDto> GetAll()
        {
            return _dbService.GetDbContext().Labs.Convert();
        }

        public LabDto GetById(int idLab)
        {
            return _dbService.GetDbContext().Labs.SingleOrDefault(e => e.IdLab == idLab)?.Convert();
        }

        public async Task<LabDto> Update(LabDto lab, int idLab)
        {
            var context = _dbService.GetDbContext();

            var entity = GetById(idLab);
            entity.Name = lab.Name;
            entity.Location = lab.Location;

            
            entity = context.Update(entity.Convert()).Entity.Convert();
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
