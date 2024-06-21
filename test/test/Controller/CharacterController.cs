using test.Services;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IDbService _dbService;

        public CharacterController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("{characterId}")]
        public async Task<IActionResult> GetCharacter(int characterId)
        {
            var character = await _dbService.GetCharacterByIdAsync(characterId);
            if (character == null) return NotFound();
            return Ok(character);
        }

        [HttpPost("{characterId}/backpacks")]
        public async Task<IActionResult> AddItemsToBackpack(int characterId, [FromBody] List<int> itemIds)
        {
            if (!await _dbService.DoesCharacterExist(characterId))
                return NotFound($"Character with given ID - {characterId} doesn't exist");

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _dbService.AddItemsToCharacterBackpackAsync(characterId, itemIds);
                    scope.Complete();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}