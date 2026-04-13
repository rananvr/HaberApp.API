using HaberApp.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HaberApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            return Ok(await _tagService.GetAllTagsAsync());
        }

        [HttpGet("{tagId}/news")]
        public async Task<IActionResult> GetNewsByTag(int tagId)
        {
            return Ok(await _tagService.GetNewsByTagIdAsync(tagId));
        }
    }
}