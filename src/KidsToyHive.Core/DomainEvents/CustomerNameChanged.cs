namespace KidsToyHive.Core.DomainEvents
{
    public class CustomerNameChanged: DomainEvent
    {
        public CustomerNameChanged(string name) => Name = name;
        public string Name { get; set; }
    }
}
