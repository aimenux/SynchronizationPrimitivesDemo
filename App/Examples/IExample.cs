using System.Threading.Tasks;

namespace App.Examples
{
    public interface IExample
    {
        string Description { get; }
        void UsePrinter();
        Task UsePrinterAsync();
    }
}