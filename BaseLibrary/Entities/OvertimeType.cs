namespace BaseLibrary.Entities
{
    public class OvertimeType : BaseEntity
    {
        public ICollection<Overtime>? Overtimes{ get; set; }
    }
}