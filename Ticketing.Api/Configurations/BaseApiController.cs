using Microsoft.AspNetCore.Mvc;

namespace Ticketing.Api.Configurations;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase { }