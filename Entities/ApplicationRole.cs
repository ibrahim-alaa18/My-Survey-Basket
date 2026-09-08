using Microsoft.AspNetCore.Identity;

namespace MySurveyBasket.Entities
{
    public sealed class ApplicationRole:IdentityRole
    {
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
    }
}
