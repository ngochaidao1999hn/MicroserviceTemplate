using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace User.Api.Models
{
    public class ApplicationRole: IdentityRole<Guid>
    {
    }
}
