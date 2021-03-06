using Game.Garage;

namespace Game.Models
{
    internal interface IGameModel : IReadGameState, ISetGameState, IPlayerSettings
    {
        IInventoryModel Equipped { get; }
        ICurrencyModel Currencies { get; }
        IPauseModel Pause { get; }
    }
}
