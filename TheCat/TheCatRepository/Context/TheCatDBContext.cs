using System.Data.Common;
using System.Data.SqlClient;
using TheCatDomain.Entities;
using TheCatDomain.Models;

namespace TheCatRepository.Context
{
    /// <summary>
    /// Classe que herda do DbContext do Entity Framework
    /// É utilizada nas classes de repositório e responsável em fazer os comandos de banco de dados
    /// </summary>
    public class TheCatDBContext
    {
        readonly AppSettings appSettings;

        /// <summary>
        /// Construtor: Recebe como parâmetro AppSettings que irá conter a string de conexão
        /// Caso queria mudar o banco de dados, basta vir nesta classe e ajustar para o novo
        /// </summary>
        /// <param name="options"></param>
        public TheCatDBContext(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        /// <summary>
        /// Cria uma conexão com a base de dados
        /// </summary>
        public DbConnection GetConnection => new SqlConnection(appSettings.ConnectionString);
    }
}
