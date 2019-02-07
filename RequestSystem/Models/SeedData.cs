using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace RequestSystem.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RequestSystemContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<RequestSystemContext>>()))
            {
                // Look for any movies.
                if (context.Note.Any())
                {
                    return;   // DB has been seeded
                }

                context.Note.AddRange(
                    new Note
                    {
                        Title = "TitleTest",
                        Description = "TestDescription",
                        CreateDate = DateTime.Parse("05.02.2019 9:00:00"),
                        Status = "Открыта"
                    },

                    new Note
                    {
                        Title = "Заголовок",
                        Description = "Описание",
                        CreateDate = DateTime.Parse("06.02.2019 9:00:00"),
                        Status = "Открыта"
                    },

                    new Note
                    {
                        Title = "Заголовок 2",
                        Description = "Описание 2",
                        CreateDate = DateTime.Parse("06.01.2019 9:00:00"),
                        Status = "Открыта"
                    },

                    new Note
                    {
                        Title = "Заголовок 3",
                        Description = "Описание проблемы",
                        CreateDate = DateTime.Parse("26.01.2019 9:00:00"),
                        Status = "Открыта"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}