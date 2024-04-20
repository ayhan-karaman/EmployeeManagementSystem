namespace BaseLibrary.Entities
{
    public class Country : BaseEntity
    {
        public ICollection<City>? Cities { get; set; }
    }
}