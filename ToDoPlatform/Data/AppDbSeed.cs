using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ToDoPlatform.Data;

public class AppDbSeed
{
    public AppDbSeed(ModelBuilder builder)
    {
        #region Popular Perfis de usuários
        List<IdentityRole> roles = new ()
        {
            new()
            {
                Id = "ff0c5ccb-7420-4626-84bc-acbe97a09cb1",
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
            },
                 new()
            {
                Id = "27d146a3-f6d3-4c2b-96be-8321d7a885a6",
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
            },
        };
       builder.Entity<IdentityRole>().HasData(roles);
        #endregion

        #region 
    } 
}

