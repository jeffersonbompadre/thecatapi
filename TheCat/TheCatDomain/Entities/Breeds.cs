using System.Collections.Generic;

namespace TheCatDomain.Entities
{
    /// <summary>
    /// Entidade utilizada para mapear a tabela Breeds no banco de dados.
    /// As propriedades são "fechadas" para aleteração, necessitando para serem alteradas utilizar um
    /// método específico que irá validar as informações
    /// </summary>
    public class Breeds
    {
        /// <summary>
        /// Construtor sem parâmetros: Necessário para o framework ORM.
        /// Ele é protegido, para que não se permita instanciar o objeto as propriedades obrigatórias
        /// </summary>
        protected Breeds()
        {
        }

        /// <summary>
        /// Construtor com as propriedades obrigatórias para o objeto existir
        /// </summary>
        /// <param name="breedsId"></param>
        /// <param name="name"></param>
        public Breeds(string breedsId, string name)
        {
            BreedsId = breedsId;
            Name = name;
        }

        public string BreedsId { get; private set; }
        public string Name { get; private set; }
        public string Origin { get; private set; }
        public string Temperament { get; private set; }
        public string Description { get; private set; }

        public ICollection<ImageUrl> Images { get; set; } = new List<ImageUrl>();

        // Métodos para atribuir informação a propriedade, validando a informação.

        public void SetBreedsId(string id)
        {
            if (IdIsValid())
                BreedsId = id;
        }

        public void SetName(string name)
        {
            if (NameIsValid())
                Name = name;
        }

        public void SetOrigin(string origin)
        {
            if (!string.IsNullOrEmpty(origin) && origin.Length <= 255)
                Origin = origin;
        }

        public void SetTemperament(string temperament)
        {
            if (!string.IsNullOrEmpty(temperament) && temperament.Length <= 255)
                Temperament = temperament;
        }

        public void SetDescription(string description)
        {
            if (!string.IsNullOrEmpty(description) && description.Length <= 1024)
                Description = description;
        }

        /// <summary>
        /// Valida se o objeto está com as informações necessárias para ser persistido na base de dados
        /// </summary>
        /// <returns></returns>
        public bool IsValid() => IdIsValid() && NameIsValid();

        // Métodos privados para consistir informações obrigatórias do objeto

        bool IdIsValid() => (!string.IsNullOrEmpty(BreedsId) && BreedsId.Length <= 80);

        bool NameIsValid() => (!string.IsNullOrEmpty(Name) && Name.Length <= 255);
    }
}
