using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using BoardGame.Services.Interfaces;
using MongoDB.Bson;
using static BoardGame.Models.DTOs.GameDTOs;

namespace BoardGame.Services
{
    public class GameService(IUnitOfWork unitOfWork) : IService, IGameService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<ObjectId> BeginNewGame(GameInfoDTO dto, string userAccount)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (dto.Player1Account != userAccount) throw new GameServiceException("GameInfo account does not match the login account!");
                await ValidateGameInfo(dto);
                
                var Id = await _unitOfWork.Games.AddAsync(dto.To<Game>());
                await _unitOfWork.CommitTransactionAsync();

                //todo: store gameId in redis

                return Id;
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

        public async Task BeginNewRound(RoundInfoDTO dto)
        {
            //todo: check gameId in redis

            dto.WhoGoesFirst = DetermineWhoGoesFirst();

            //todo: store round information in redis
        }

        public WhoGoesFirst DetermineWhoGoesFirst()
        {
            var random = new Random().Next(1);
            return (WhoGoesFirst)random;
        }

        public Func<PlayerRoundInfo, PlayerRoundInfo, Result> MapRule(Character character)
        {
            return character switch
            {
                Character.Assassin => Assassin.Rule,
                Character.Deceiver => Deceiver.Rule,
                Character.Knight => Knight.Rule,
                Character.Lobbyist => Lobbyist.Rule,
                Character.Lord => Lord.Rule,
                Character.Soldier => Soldier.Rule,
                _ => throw new NotImplementedException()
            };
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
