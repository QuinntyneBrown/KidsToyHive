namespace KidsToyHive.Core.DomainEvents
{
    public class BrandNameChanged: DomainEvent
    {
        public BrandNameChanged(string name) {
            Name = name;
        }
        public string Name { get; set; }
    }
}
