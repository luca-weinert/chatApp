namespace ChatApp.Shared.User
{
    public class User
    {
        public Guid Id { get; init; } = Guid.NewGuid(); // Ensures Id is set once and cannot be modified
        public string Name { get; set; }

        public User(string name)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Name cannot be empty", nameof(name));
        }
        public override string ToString() => $"User(Id: {Id}, Name: {Name})";
    }
}