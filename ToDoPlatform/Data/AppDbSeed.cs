using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoPlatform.Models;

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

        #region Popular dados de Usuário
        List<AppUser> users = new()
        {
            new AppUser()
            {
                Id = "405faf1b-5420-493c-a3f9-aa05e7247a43",
                Email = "nicolyathayde4@gmail.com",
                NormalizedEmail = "NICOLYATHAYDE4@GMAIL",
                UserName = "nicolyathayde4@gmail.com",
                NormalizedUserName = "NICOLYATHAYDE4@GMAIL.COM",
                LockoutEnabled = false,
                EmailConfirmed = true,
                Name = "Nicoly Athayde Silva Oliveira",
                ProfilePicture = "/img/user/405faf1b-5420-493c-a3f9-aa05e7247a43.png"
            },
            new AppUser()
            {
                Id = "1830a86c-dea3-460a-9538-222fc1c83063",
                Email = "amandabordotti12@gmail.com",
                NormalizedEmail = "AMANDABORDOTTI12@GMAIL",
                UserName = "amandabordotti12@gmail.com",
                NormalizedUserName = "AMANDABORDOTTI12@GMAIL.COM",
                LockoutEnabled = true,
                EmailConfirmed = true,
                Name = "Amanda Bordotti",
                ProfilePicture = "/img/user/1830a86c-dea3-460a-9538-222fc1c83063.png"
            },
        };
        foreach (var user in users)
        {
            PasswordHasher<IdentityUser> pass = new();
            user.PasswordHash = pass.HashPassword(user, "123456");
        }
        builder.Entity<AppUser>().HasData(users);
        #endregion

        #region Popular Dados de Usuário Perfil
        List<IdentityUserRole<string>> userRoles = new()
        {
            new IdentityUserRole<string>()
            {
                UserId = users[0].Id,
                RoleId = roles[0].Id
            },
            new IdentityUserRole<string>()
            {
                UserId = users[1].Id,
                RoleId = roles[1].Id
            },
        };
        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        #endregion

        #region Popular as Tarefas do usuário
        List<ToDo> toDos = new()
        {
            new ToDo()
            {
                Id = 1,
                Title = "Terminar coreografia das crianças",
                Description = "Fazer uma sequência em aula",
                UserId = users[1].Id
            },
            new ToDo()
            {
                Id = 2,
                Title = "Responder o formulário",
                Description = "Urgente",
                UserId = users[1].Id
            },
            new ToDo()
            {
                Id = 3,
                Title = "Passar uma diagonal",
                Description = "Não esquecer",
                UserId = users[1].Id
            },
        };
        builder.Entity<ToDo>().HasData(toDos);
        #endregion
    } 
}

