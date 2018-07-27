namespace KidsToyHive.Core.DomainEvents
{
    public class OrderItemNameChanged: DomainEvent
    {
        public OrderItemNameChanged(string name) => Name = name;
        public string Name { get; set; }
    }
}
