using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DepremVeriProjesi.Models
{
    public class Deprem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("tarih")]
        public DateTime Tarih { get; set; }

        [BsonElement("lokasyon")]
        public string? Lokasyon { get; set; }

        [BsonElement("buyukluk")]
        public double Buyukluk { get; set; }

        [BsonElement("derinlik")]
        public double Derinlik { get; set; }
    }
}
