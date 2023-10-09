using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NotesUsersApi.Controllers
{
    [Route("[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        protected Guid UserId => !User.Identity.IsAuthenticated 
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
