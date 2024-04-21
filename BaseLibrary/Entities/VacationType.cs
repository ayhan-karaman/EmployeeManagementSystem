namespace BaseLibrary.Entities
{
    public class VacationType : BaseEntity
    {
        // Manyt to one relationship with Vacation
        public ICollection<Vacation>? Vacations{ get; set; }
    }
}