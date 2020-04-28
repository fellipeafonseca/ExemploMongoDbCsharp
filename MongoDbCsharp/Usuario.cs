using MongoDB.Bson;

namespace MongoDbCsharp
{
    public class Usuario
    {
        public ObjectId _id { get; set; }
        public string login { get; set; }
        public string senha { get; set; }

        public bool ativo { get; set; }

        public override string ToString()
        {
            return $"Usuario: {login}, Ativo: {ativo}";
        }
    }
}