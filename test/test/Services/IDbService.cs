using test.DTO;
using test.Models;

namespace test.Services;

public interface IDbService
{
    Task<bool> DoesCharacterExist(int characterId);
    Task<Characters?> GetCharacterByIdAsync(int characterId);
    Task<bool> ItemsExist(List<int> itemIdList);
    Task<int> GetWeight(List<int> itemIdList);
    Task AddItemsToCharacterBackpackAsync(int characterId, List<int> itemIdList);
}