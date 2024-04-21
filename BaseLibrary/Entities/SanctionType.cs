namespace BaseLibrary.Entities
{
    public class SanctionType : BaseEntity
    {
        public ICollection<Sanction>? Sanctions{ get; set; }
    }
}