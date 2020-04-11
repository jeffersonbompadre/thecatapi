using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCatDomain.Entities;
using TheCatDomain.Interfaces.Repositories;
using TheCatRepository.Context;

namespace TheCatRepository.Repositories
{
    public class BreedsRepository : IBreedsRepository
    {
        readonly TheCatContext theCatContext;

        public BreedsRepository(TheCatContext theCatContext)
        {
            this.theCatContext = theCatContext;
        }

        public async Task<ICollection<Breeds>> GetAllBreeds()
        {
            return await theCatContext.Breeds.AsNoTracking().ToListAsync();
        }

        public async Task<Breeds> GetBreeds(string id)
        {
            return await theCatContext.Breeds.FirstOrDefaultAsync(x => x.BreedsId == id);
        }

        public async Task AddBreeds(Breeds breeds)
        {
            if (!breeds.IsValid())
                return;
            else
            {
                theCatContext.Add(breeds);
                await theCatContext.SaveChangesAsync();
            }
        }

        public async Task UpdateBreeds(Breeds breeds)
        {
            if (!breeds.IsValid())
                return;
            else
            {
                theCatContext.Entry(breeds).State = EntityState.Modified;
                await theCatContext.SaveChangesAsync();
            }
        }
    }
}
