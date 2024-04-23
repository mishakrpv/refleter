using Microsoft.AspNetCore.Mvc;

namespace Storage.API.Controllers;

[ApiController]
[Route("api/v1/storage/[controller]")]
public abstract class RootController : ControllerBase;