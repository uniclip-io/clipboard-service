using ClipboardApi.Dtos;
using ClipboardApi.Models;
using ClipboardApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClipboardApi.Controllers;

[ApiController]
[Route("clipboard")]
public class ClipboardController : ControllerBase
{
    private readonly ClipboardService _clipboardService;

    public ClipboardController(ClipboardService clipboardService)
    {
        _clipboardService = clipboardService;
    }

    [HttpGet("/fetch/{userId:guid}")]
    public async Task<ActionResult<Clipboard>> GetClipboard(Guid userId)
    {
        var clipboard = await _clipboardService.GetClipboardByUserId(userId);

        if (clipboard == null)
        {
            return NotFound();
        }
        return Ok(clipboard);
    }
    
    [HttpPost("/post")]
    public async Task<ActionResult<Record>> PostClipboardContent(PostClipboardContent postClipboardContent)
    {
        var userId = postClipboardContent.userId;
        var contentType = postClipboardContent.contentType;
        var content = postClipboardContent.content;

        var clipboard = await _clipboardService.GetClipboardByUserId(userId) ??
                        await _clipboardService.CreateClipboard(userId);
        var record = await _clipboardService.AddContentToClipboard(clipboard.Id, contentType, content);

        return Ok(record);
    }
}