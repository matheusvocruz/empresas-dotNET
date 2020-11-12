using FluentValidation.Results;
using System.Threading.Tasks;

namespace IMDb.Core.Interfaces
{
    public interface IMediatorHandler
    {
        Task<ValidationResult> SendCommand<T>(T comand) where T : Command;
    }
}
