namespace db.DbEntities
{
    public class Raport
    {
        public int Type { get; set; }
        public DateTime FormDate { get; set; }
        public List<Product> Products { get; set; }
        public string Responsables {  get; set; }
        public string FormPlace { get; set; }
    }
}
