using Microsoft.EntityFrameworkCore;
using test.Data;
using test.DTO;
using test.Models;

namespace test.Services
{
    public class DbService : IDbService
    {
        private readonly DatabaseContext _db;

        public DbService(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<bool> DoesCharacterExist(int characterId)
        {
            return await _db.Characters.AnyAsync(ch => ch.Id == characterId);
        }

        public async Task<Characters?> GetCharacterByIdAsync(int characterId)
        {
            return await _db.Characters.FirstOrDefaultAsync(ch => ch.Id == characterId);
        }

        public async Task<List<BackpackItemDto>> GetItemsForC(int characterId)
        {
            var itemsList = await _db.Backpacks
                .Where(bp => bp.CharacterId == characterId)
                .Join(
                    _db.Items,
                    bp => bp.ItemId,
                    itm => itm.Id,
                    (bp, itm) => new BackpackItemDto
                    {
                        ItemName = itm.Name,
                        ItemWeight = itm.Weight,
                        Amount = bp.Amount
                    }
                ).ToListAsync();

            return itemsList;
        }

        

        public async Task<bool> ItemsExist(List<int> itemIdList)
        {
            return await _db.Items.CountAsync(itm => itemIdList.Contains(itm.Id)) == itemIdList.Count;
        }

        public async Task<int> GetWeight(List<int> itemIdList)
        {
            return await _db.Items
                .Where(itm => itemIdList.Contains(itm.Id))
                .SumAsync(itm => itm.Weight);
        }

        public async Task AddItemsToCharacterBackpackAsync(int characterId, List<int> itemIdList)
        {
            var characterEntity = await _db.Characters.FindAsync(characterId);
            if (characterEntity == null)
            {
                throw new ArgumentException("Character not found");
            }

            var newBackpackItems = itemIdList.Select(itemId => new Backpacks
            {
                CharacterId = characterId,
                ItemId = itemId,
                Amount = 1
            }).ToList();

            _db.Backpacks.AddRange(newBackpackItems);

            var addedWeight = await GetWeight(itemIdList);
            characterEntity.CurrentWeight += addedWeight;

            await _db.SaveChangesAsync();
        }
    }
}
