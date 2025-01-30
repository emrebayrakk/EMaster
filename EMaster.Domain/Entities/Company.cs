namespace EMaster.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<User> Users { get; set; } = new List<User>();
    }
}
