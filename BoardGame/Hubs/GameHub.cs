using BoardGame.Authorizations;
using BoardGame.Infrastractures;
using BoardGame.Models.DTOs;
using BoardGame.Services;
using BoardGame.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Utility;

namespace BoardGame.Hubs;

[AuthorizeRoles(Role.Member, Role.Guest)]
public class GameHub(IGameService gameService) : Hub
{
    public async Task PlayWithBot()
    {
        var account = GetUserAccount();

        var roomId = await gameService.PlayWithBot(account);
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

        await Clients.Group(roomId).SendAsync("ReceiveQueue", "game started");
    }

    public async Task StartQueuing()
    {
        try
        {
            var account = GetUserAccount();
            var (gameStarted, roomId) = await gameService.Match(account);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            if (gameStarted) 
            {
                await Clients.Group(roomId).SendAsync("ReceiveQueue", "game started");
            }
        }
        catch (GameServiceException ex)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveGameServiceException", ex.Message);
        }
        catch (Exception e)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveException", e.Message);
        }
    }

    //public async Task HostGame()
    //{
    //    try
    //    {
    //        var roomId = await gameService.HostGame();
    //        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
    //    }
    //    catch (Exception ex)
    //    {
    //        await FailHosting(ex.Message);
    //    }
    //}

    //public async Task FailHosting(string message)
    //{
    //    await Clients.Client(Context.ConnectionId).SendAsync("FailHosting", message);
    //}

    //public async Task JoinGame()
    //{
    //    var roomId = await gameService.JoinGame();

    //    if (string.IsNullOrEmpty(roomId))
    //    {
    //        await Clients.Client(Context.ConnectionId).SendAsync("NoRoom", "No room");
    //        return;
    //    }

    //    await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
    //}

    //public async Task GetReady(string roomId)
    //{
    //    string userAccount = GetUserAccount();

    //    var ready = gameService.GetReady(userAccount);
    //    await Clients.OthersInGroup(roomId).SendAsync("RecievedReady", ready);
    //}

    public async Task EndGame(string currentGameId)
    {
        try
        {
            string userAccount = GetUserAccount();

            await gameService.EndGame(currentGameId, userAccount);

        }
        catch (GameServiceException ex)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveGameServiceException", ex.Message);
        }
        catch (Exception e)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveException", e.Message);
        }
    }

    public async Task BeginNewRound(RoundInfoRequestDTO request)
    {
        try
        {
            string userAccount = GetUserAccount();

            await gameService.BeginNewRound(request.To<RoundInfoDTO>(), userAccount);

        }
        catch (GameServiceException ex)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveGameServiceException", ex.Message);
        }
        catch (Exception e)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveException", e.Message);
        }
    }

    public async Task OpenNextCard(OpenNextCardRequestDTO dto)
    {
        try
        {
            dto.UserAccount = GetUserAccount();

            var (card, IsLastCardOpened) = await gameService.OpenNextCard(dto);

            await Clients.OthersInGroup(dto.CurrentGameId).SendAsync("ReceiveNextCard", card);

            if (IsLastCardOpened)
            {
                await gameService.EndRound(dto.CurrentGameId, dto.UserAccount);
            }
        }
        catch(GameServiceException ex)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveGameServiceException", ex.Message);
        }
        catch (Exception e)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveException", e.Message);
        }
    }

    public async Task EndRound(string currentGameId)
    {
        try
        {
            string userAccount = GetUserAccount();

            await gameService.EndRound(currentGameId, userAccount);

            await Clients.Group(currentGameId).SendAsync("ReceiveEndRound", card);
        }
        catch (GameServiceException ex)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveGameServiceException", ex.Message);
        }
        catch (Exception e)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveException", e.Message);
        }
    }

    private string GetUserAccount()
    {
        string userAccount = Context.GetJwtClaim(ClaimTypes.Name).Value;

        if (string.IsNullOrEmpty(userAccount))
        {
            throw new Exception(ErrorCode.InvalidAccount);
        }

        return userAccount;
    }
}
