
namespace CleanArchitecture.Domain.Enitites
{
    public sealed class Blog
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Author { get; private set; }
        public string? ImageUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public Blog(string name, string description, string author)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Name = name;
            Description = description;
            Author = author;
        }
        ///<summary>
        /// the methods below are not being applied because the service is implementing it differently.
        /// pt-br: the methods below are not being applied because the service is implementing it differently.
        /// In a robust project, it is also necessary to create a class that will extend Exception and perform domain validations using this class.
        /// pt-br = Em um projeto robusto, também é necessário criar uma classe que estenda Exception e execute validações de domínio usando esta classe.
        /// </summary>
        public void Update(string name, string description, string author, string? imageUrl = null)
        {
            Name = name;
            Description = description;
            Author = author;
            ImageUrl = imageUrl;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete()
        {
            DeletedAt = DateTime.UtcNow;
        }
    }
}
