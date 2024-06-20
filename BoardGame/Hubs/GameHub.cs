using BoardGame.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace BoardGame.Hubs;

public class GameHub(IGameService gameService) : Hub
{
    public async Task HostGame()
    {
        var gameId = await gameService.HostGame();
        await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
    }

    public async Task JoinGame(string gameId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
    }
}
