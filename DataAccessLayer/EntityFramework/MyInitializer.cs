using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {

            Users user1 = new Users()
            {
                Name = "Habip",
                Surname = "AKGÜL",
                Email = "habipakgul@gmail.com",
                Username = "habip",
                Degree = "Jeoloji Mühendisi",
                Task = "Şirket Yöneticisi",
                Password = "liceli21",
                IsAdmin = true,
                ProfilImage = "user.png",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "system"
            };

            context.Users.Add(user1);

            Users user2 = new Users()
            {
                Name = "Welat",
                Surname = "BARAN",
                Email = "baranvelat021@gmail.com",
                Username = "welat",
                Degree = "Bilgisayar Mühendisi",
                Task = "Şirket Yöneticisi",
                Password = "liceli21",
                IsAdmin = false,
                ProfilImage = "user.png",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "system"
            };

            context.Users.Add(user2);
            context.SaveChanges();
        }
    }
}
