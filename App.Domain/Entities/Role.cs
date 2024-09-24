namespace App.Domain.Entities
{
    public class Role
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Role(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
