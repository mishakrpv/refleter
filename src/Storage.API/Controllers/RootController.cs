using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Storage.API.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/storage/[controller]")]
public abstract class RootController : ControllerBase;