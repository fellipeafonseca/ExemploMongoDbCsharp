using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDbCsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();

            try
            {
                program.ConexaoMongoDb();
              //   program.Inseir();
                // program.Alterar();

                // program.AdcionarCampoColecao();

                program.Listar();
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine($"Erro, não foi conectar ao servidor : {ex.Message}");
            }



        }

        IMongoClient client;
        IMongoDatabase database;
        IMongoCollection<Usuario> colecao;

        public void ConexaoMongoDb()
        {
            var settings = new MongoClientSettings
            {
                //    ServerSelectionTimeout = new TimeSpan(0, 0, 5),
                Server = new MongoServerAddress("localhost", 27017),
                //Credentials = new[]
                //{
                //    MongoCredential.CreateCredential("loja","felipe","1234")
                //}
            };

            // client = new MongoClient(
            //   "mongodb://localhost:27017");

            client = new MongoClient(settings);
            //Banco de Dados se não existir é criado
            database = client.GetDatabase("loja");

            //Pega as Coleções equivalente a tabelas
            colecao = database.GetCollection<Usuario>("usuario");

        }

        public void Inseir()
        {

            var usuarios = new List<Usuario>
            {
                new Usuario {
                    login = "fellipe", senha = "123", ativo = false
                },
                new Usuario{
                    login = "celso", senha = "abcd", ativo = true
                },

                 new Usuario{
                    login = "maria", senha = "5678", ativo = true
                }
            };

            //Inserir um Usuario
            //colecao.InsertOne(usuario);

            //Inserir mais de um Usuario
             colecao.InsertMany(usuarios);


            Console.WriteLine("Documentos Inseridos");

        }


        public void Alterar()
        {
            var filtro = Builders<Usuario>.Filter.Eq(u => u.login, "felipe");
            var alteracao = Builders<Usuario>.Update.Set(u => u.senha, "abcdf");

            //Alterar 1 item
            // colecao.UpdateOne(filtro, alteracao);

            //Alterar mais de 1 Item
            colecao.UpdateMany(filtro, alteracao);

            Console.WriteLine("Documento Alterado");
        }


        public void Excluir()
        {
            var filtro = Builders<Usuario>.Filter.Eq(u => u.ativo, true);

            //var filtro = Builders<Usuario>.Filter.Where(u =>
            //  u.login.Equals("felipe") || u.login.Equals("diego"));

            var resultado = colecao.DeleteMany(filtro);

            //Obtem o Resultado de quantos itens foram excluidos
            //resultado.DeletedCount

            Console.WriteLine("Documentos Excluidos");
        }


        public void Listar()
        {
            var filtro = Builders<Usuario>.Filter.Empty;

            var resultado = colecao.Find(filtro).ToList();

            resultado.ForEach(u => Console.WriteLine(u.ToString()));
        }

        public void AdcionarCampoColecao()
        {
            var filtro = Builders<Usuario>.Filter.Empty;

            var alteracao = Builders<Usuario>.Update.Set(u => u.ativo, true);

            colecao.UpdateMany(filtro, alteracao);

            Console.WriteLine("Documento Alterado com novo Campo");
        }
    }
}
