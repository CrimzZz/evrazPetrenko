namespace db.DbEntities
{
    public class DefectData
    {
        public Defect Defect { get; set; }
        public double DefectLength { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
