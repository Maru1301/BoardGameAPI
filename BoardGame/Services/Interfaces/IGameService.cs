using BoardGame.Infrastractures;
using static BoardGame.Models.ViewModels.GameVMs;

namespace BoardGame.Services.Interfaces
{
    public interface IGameService
    {
        public Func<PlayerInfoVM, PlayerInfoVM, Result> MapRule(Character character);
    }
}
