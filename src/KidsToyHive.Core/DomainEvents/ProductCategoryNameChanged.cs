namespace KidsToyHive.Core.DomainEvents
{
    public class ProductCategoryNameChanged: DomainEvent
    {
        public ProductCategoryNameChanged(string name) => Name = name;
        public string Name { get; set; }
    }
}
