namespace KidsToyHive.Core.DomainEvents
{
    public class OrderNameChanged: DomainEvent
    {
        public OrderNameChanged(string name) => Name = name;
        public string Name { get; set; }
    }
}
