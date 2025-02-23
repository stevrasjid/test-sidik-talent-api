using Microsoft.AspNetCore.Mvc;

public class BaseApiController : ControllerBase 
{
    public IActionResult ApiResponse<T>(T data, string message = "Success") 
    {
        return Ok(new {message, data});
    }

    public IActionResult BadRequest(string message = "Error") {
        return BadRequest(new {message});
    }
}