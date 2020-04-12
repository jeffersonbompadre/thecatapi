namespace TheCatDomain.Entities
{
    /// <summary>
    /// Entidade utilizada para mapear a tabela ImageUrl no banco de dados.
    /// As propriedades são "fechadas" para aleteração, necessitando para serem alteradas utilizar um
    /// método específico que irá validar as informações
    /// </summary>
    public class ImageUrl
    {
        /// <summary>
        /// Construtor sem parâmetros: Necessário para o framework ORM.
        /// Ele é protegido, para que não se permita instanciar o objeto as propriedades obrigatórias
        /// </summary>
        protected ImageUrl()
        {
        }

        /// <summary>
        /// Construtor com as propriedades obrigatórias para o objeto existir
        /// </summary>
        /// <param name="imageUrlId"></param>
        /// <param name="url"></param>
        public ImageUrl(string imageUrlId, string url)
        {
            ImageUrlId = imageUrlId;
            Url = url;
        }

        public string ImageUrlId { get; private set; }
        public string Url { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Breeds Breeds { get; private set; }
        public Category Category { get; private set; }

        // Métodos para atribuir informação a propriedade, validando a informação.

        public void SetImageUrlId(string id)
        {
            if (IdIsValid())
                ImageUrlId = id;
        }

        public void SetUrl(string url)
        {
            if (UrlIsValid())
                Url = url;
        }

        public void SetWidth(int width)
        {
            if (width > 0)
                Width = width;
        }

        public void SetHeight(int height)
        {
            if (height > 0)
                Height = height;
        }

        public void SetBreeds(Breeds breeds)
        {
            if (breeds != null)
                Breeds = breeds;
        }

        public void SetCategory(Category category)
        {
            if (category != null)
                Category = category;
        }

        /// <summary>
        /// Valida se o objeto está com as informações necessárias para ser persistido na base de dados
        /// </summary>
        /// <returns></returns>
        public bool IsValid() => IdIsValid() && UrlIsValid();

        // Métodos privados para consistir informações obrigatórias do objeto

        bool IdIsValid() => (!string.IsNullOrEmpty(ImageUrlId) && ImageUrlId.Length <= 80);

        bool UrlIsValid() => (!string.IsNullOrEmpty(Url) && Url.Length <= 512);
    }
}
