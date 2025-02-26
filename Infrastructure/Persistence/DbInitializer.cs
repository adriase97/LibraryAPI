using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            if (context.Authors.Any() || context.Books.Any() || context.Publishers.Any() || context.BookPublishers.Any()) return;

            #region Roles
            foreach (Role role in Enum.GetValues(typeof(Role))) await roleManager.CreateAsync(new IdentityRole(role.ToString()));
            #endregion

            #region Users
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                var user = new IdentityUser { UserName = role.ToString(), Email = $"{role.ToString()}@library.com" };
                var result = await userManager.CreateAsync(user, $"{role.ToString()}@123");
                if (result.Succeeded) await userManager.AddToRoleAsync(user, role.ToString());
            }
            #endregion

            #region Authors
            var authors = new List<Author>
            {
                new Author { Name = "Albert Camus", Nationality = "French", BirthDate = new DateTime(1913, 11, 7), Biography = "French philosopher and writer, known for 'The Stranger' and 'The Myth of Sisyphus'." },
                new Author { Name = "Fyodor Dostoyevsky", Nationality = "Russian", BirthDate = new DateTime(1821, 11, 11), Biography = "Russian novelist, famous for 'Crime and Punishment' and 'The Brothers Karamazov'." },
                new Author { Name = "Friedrich Nietzsche", Nationality = "German", BirthDate = new DateTime(1844, 10, 15), Biography = "German philosopher, known for 'Thus Spoke Zarathustra' and 'Beyond Good and Evil'." },
                new Author { Name = "Franz Kafka", Nationality = "Austrian", BirthDate = new DateTime(1883, 7, 3), Biography = "Austrian writer, famous for 'The Metamorphosis' and 'The Trial'." },
                new Author { Name = "Johann Wolfgang von Goethe", Nationality = "German", BirthDate = new DateTime(1749, 8, 28), Biography = "German writer and philosopher, best known for 'Faust' and 'The Sorrows of Young Werther'." }
            };
            #endregion

            #region Books
            var books = new List<Book>
            {
                // Albert Camus
                new Book { Title = "The Stranger", PublicationYear = 1942, ISBN = "9780679720201", Pages = 123, Genre = Genre.PhilosophicalFiction, Price = 12.99m, Author = authors[0] },
                new Book { Title = "The Myth of Sisyphus", PublicationYear = 1942, ISBN = "9780141023991", Pages = 160, Genre = Genre.Philosophy, Price = 14.99m, Author = authors[0] },
                new Book { Title = "The Plague", PublicationYear = 1947, ISBN = "9780679720218", Pages = 308, Genre = Genre.PhilosophicalFiction, Price = 15.99m, Author = authors[0] },
                
                // Fyodor Dostoyevsky
                new Book { Title = "Crime and Punishment", PublicationYear = 1866, ISBN = "9780486415871", Pages = 671, Genre = Genre.PsychologicalFiction, Price = 17.99m, Author = authors[1] },
                new Book { Title = "The Brothers Karamazov", PublicationYear = 1880, ISBN = "9780140449242", Pages = 824, Genre = Genre.PsychologicalFiction, Price = 19.99m, Author = authors[1] },
                new Book { Title = "Notes from Underground", PublicationYear = 1864, ISBN = "9780486270531", Pages = 128, Genre = Genre.ExistentialFiction, Price = 11.99m, Author = authors[1] },
                
                // Friedrich Nietzsche
                new Book { Title = "Thus Spoke Zarathustra", PublicationYear = 1883, ISBN = "9780140441185", Pages = 352, Genre = Genre.Philosophy, Price = 13.99m, Author = authors[2] },
                new Book { Title = "Beyond Good and Evil", PublicationYear = 1886, ISBN = "9780140449235", Pages = 240, Genre = Genre.Philosophy, Price = 12.49m, Author = authors[2] },
                new Book { Title = "The Antichrist", PublicationYear = 1895, ISBN = "9780140449236", Pages = 96, Genre = Genre.Philosophy, Price = 9.99m, Author = authors[2] },
                
                // Franz Kafka
                new Book { Title = "The Metamorphosis", PublicationYear = 1915, ISBN = "9780486290300", Pages = 201, Genre = Genre.Surrealism, Price = 10.99m, Author = authors[3] },
                new Book { Title = "The Trial", PublicationYear = 1925, ISBN = "9780805209990", Pages = 304, Genre = Genre.AbsurdistFiction, Price = 13.99m, Author = authors[3] },
                new Book { Title = "The Castle", PublicationYear = 1926, ISBN = "9780805211061", Pages = 352, Genre = Genre.ModernistFiction, Price = 14.99m, Author = authors[3] },
                
                // Johann Wolfgang von Goethe
                new Book { Title = "Faust", PublicationYear = 1808, ISBN = "9780140449013", Pages = 512, Genre = Genre.Tragedy, Price = 16.99m, Author = authors[4] },
                new Book { Title = "The Sorrows of Young Werther", PublicationYear = 1774, ISBN = "9780812969900", Pages = 144, Genre = Genre.Romanticism, Price = 11.99m, Author = authors[4] },
                new Book { Title = "Wilhelm Meister’s Apprenticeship", PublicationYear = 1795, ISBN = "9780140446012", Pages = 608, Genre = Genre.Bildungsroman, Price = 18.99m, Author = authors[4] }
            };
            #endregion

            #region Publisher
            var publishers = new List<Publisher>
            {
                new Publisher { Name = "Penguin Classics", Country = "United Kingdom", FoundedYear = 1946, Website = "https://www.penguin.co.uk" },
                new Publisher { Name = "Vintage Books", Country = "United States", FoundedYear = 1954, Website = "https://www.vintagebooks.com" },
                new Publisher { Name = "Oxford University Press", Country = "United Kingdom", FoundedYear = 1586, Website = "https://www.oup.com" },
                new Publisher { Name = "Suhrkamp Verlag", Country = "Germany", FoundedYear = 1950, Website = "https://www.suhrkamp.de" }
            };

            #endregion

            #region BookPublishers
            var bookPublishers = new List<BookPublisher>
            {
                // Penguin Classics
                new BookPublisher { Book = books[0], Publisher = publishers[0], PublishedDate = DateTime.Now },
                new BookPublisher { Book = books[1], Publisher = publishers[0], PublishedDate = DateTime.Now },

                // Vintage Books
                new BookPublisher { Book = books[2], Publisher = publishers[1], PublishedDate = DateTime.Now },
                new BookPublisher { Book = books[3], Publisher = publishers[1], PublishedDate = DateTime.Now },

                // Oxford University Press
                new BookPublisher { Book = books[4], Publisher = publishers[2], PublishedDate = DateTime.Now },
                new BookPublisher { Book = books[5], Publisher = publishers[2], PublishedDate = DateTime.Now },

                // Suhrkamp Verlag
                new BookPublisher { Book = books[6], Publisher = publishers[3], PublishedDate = DateTime.Now },
                new BookPublisher { Book = books[7], Publisher = publishers[3], PublishedDate = DateTime.Now }
            };
            #endregion

            #region Add Range
            if (!context.Authors.Any()) await context.Authors.AddRangeAsync(authors);
            if (!context.Books.Any()) await context.Books.AddRangeAsync(books);
            if (!context.Publishers.Any()) await context.Publishers.AddRangeAsync(publishers);
            if (!context.BookPublishers.Any()) await context.BookPublishers.AddRangeAsync(bookPublishers);

            await context.SaveChangesAsync();
            #endregion
        }
    }
}
