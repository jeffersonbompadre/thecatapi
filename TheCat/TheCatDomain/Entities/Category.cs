using System.Collections.Generic;

namespace TheCatDomain.Entities
{
    /// <summary>
    /// Entidade utilizada para mapear a tabela Category no banco de dados.
    /// As propriedades são "fechadas" para aleteração, necessitando para serem alteradas utilizar um
    /// método específico que irá validar as informações
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Construtor sem parâmetros: Necessário para o framework ORM.
        /// Ele é protegido, para que não se permita instanciar o objeto as propriedades obrigatórias
        /// </summary>
        protected Category()
        {
        }

        /// <summary>
        /// Construtor com as propriedades obrigatórias para o objeto existir
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="name"></param>
        public Category(int categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }

        public int CategoryId { get; private set; }
        public string Name { get; private set; }

        public ICollection<ImageUrl> Images { get; set; } = new List<ImageUrl>();

        // Métodos para atribuir informação a propriedade, validando a informação.

        public void SetCategoryId(int id)
        {
            if (IdIsValid())
                CategoryId = id;
        }

        public void SetName(string name)
        {
            if (NameIsValid())
                Name = name;
        }

        /// <summary>
        /// Valida se o objeto está com as informações necessárias para ser persistido na base de dados
        /// </summary>
        /// <returns></returns>
        public bool IsValid() => IdIsValid() && NameIsValid();

        // Métodos privados para consistir informações obrigatórias do objeto

        bool IdIsValid() => (CategoryId > 0);

        bool NameIsValid() => (!string.IsNullOrEmpty(Name) && Name.Length <= 255);
    }
}
