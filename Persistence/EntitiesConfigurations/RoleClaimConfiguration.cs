using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyBasket.Abstractions.Consts;

namespace SurveyBasket.Persistence.EntitiesConfigurations;

public class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        //Default Data
        var adminClaims = new List<IdentityRoleClaim<string>>();

        for(var i = 0; i < Permissions.All.Count; i++)
        {
            adminClaims.Add(new IdentityRoleClaim<string>
            {
                Id = i + 1,
                ClaimType = Permissions.Type,
                ClaimValue = Permissions.All[i],
                RoleId = DefaultRoles.AdminRoleId
            });
        }

        builder.HasData(adminClaims);
    }
}