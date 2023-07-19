

using System.ComponentModel.DataAnnotations.Schema;

namespace db.DbEntities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Claim { get; set; }
        public IDictionary<string, string>? Properties { get; set; }
        public Profile? Profile { get; set; }
        public Brand? Brand { get; set; }
    }
}
