using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace tripscribe.Api.Controllers;

[ExcludeFromCodeCoverage]
[ApiController]
public class TripscribeBaseController : ControllerBase
{
    protected ActionResult OkOrNoContent(object value)
    {
        if (HasNoValueOrItems(value)) return NoContent();
            
        return Ok(value);
    }
        
    protected  ActionResult OkOrNoNotFound(object value)
    {
        if (HasNoValueOrItems(value)) return NotFound();
            
        return Ok(value);
    }

    private static bool HasNoValueOrItems(object value) => value == null || value is IList {Count: < 1};
}