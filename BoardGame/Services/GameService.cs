﻿using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using BoardGame.Services.Interfaces;
using static BoardGame.Models.DTOs.GameDTOs;

namespace BoardGame.Services
{
    public class GameService(IUnitOfWork unitOfWork) : IService, IGameService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task BeginNewGame(GameInfoDTO dto, string userAccount)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (dto.Player1Account != userAccount) throw new GameServiceException("GameInfo account does not match the login account!");
                await ValidateGameInfo(dto);
                
                await _unitOfWork.Games.AddAsync(dto.ToEntity<Game>());
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (GameServiceException)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public WhoGoesFirst DetermineWhoGoesFirst()
        {
            var random = new Random().Next(1);
            return (WhoGoesFirst)random;
        }

        private async Task ValidateGameInfo(GameInfoDTO dto)
        {
            _ = await _unitOfWork.Members.GetByAccountAsync(dto.Player1Account) ?? throw new GameServiceException("player1 account does not exist!");
            _ = await _unitOfWork.Members.GetByAccountAsync(dto.Player1Account) ?? throw new GameServiceException("player2 account does not exist!");

            var chosenCharacters1 = new HashSet<Character>
            {
                dto.Player1Characters.Character1,
                dto.Player1Characters.Character2,
                dto.Player1Characters.Characetr3 
            };
            var chosenCharacters2 = new HashSet<Character>
            {
                dto.Player2Characters.Character1,
                dto.Player2Characters.Character2,
                dto.Player2Characters.Characetr3
            };

            if (chosenCharacters1.Count != 3 || chosenCharacters2.Count != 3) throw new GameServiceException("Duplicated characters");
        }
    }

    public class GameServiceException(string message) : Exception(message)
    {
    }
}
