using System.Threading.Tasks;

namespace TheCatDomain.Interfaces.Application
{
    /// <summary>
    /// Interface que especifica o contrato que deve ser seguido para implementar os métodos
    /// necessários para capturar informações da TheCatAPI e armazenar na base de dados
    /// </summary>
    public interface ICommandCapture
    {
        Task CapureAllBreedsWithImages();
        Task CaptureImagesByCategory();
    }
}
