using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using BoardGame.Services.Interfaces;
using BoardGame.Models.DTOs;
using Utility;
using System.Security.Cryptography;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BoardGame.Services
{
    public class GameService(IUnitOfWork unitOfWork, ICacheService cacheService) : IService, IGameService
    {
        private const string _bot = "Bot";

        public async Task<IEnumerable<GameDTO>> GetGameRecordList()
        {
            try
            {
                var games = await unitOfWork.Games.GetAllAsync();

                return games.Select(x => x.To<GameDTO>());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> PlayWithBot(string account)
        {
            var roomId = string.Empty;
            var roomIdUsed = true;
            while (roomIdUsed)
            {
                roomId = $"Room_{RandomNumberGenerator.GetHexString(16, true)}";
                var cacheData = await cacheService.StringGetAsync(roomId);

                roomIdUsed = !string.IsNullOrEmpty(cacheData);
            }

            var whoGoesFirst = DetermineWhoGoesFirst();

            var dto = new GameInfoDTO()
            {
                Player1Account = account,
                Player2Account = _bot,
            };

            if (whoGoesFirst == WhoGoesFirst.Player2)
            {
                dto = new GameInfoDTO()
                {
                    Player1Account = _bot,
                    Player2Account = account,
                };
            }
            
            await BeginNewGame(dto);

            return roomId;
        }

        public async Task<(bool, string)> Match(string account)
        {
            var player = await cacheService.ListLeftPopAsync(CacheKey.WaitingPlayer);

            var roomId = string.Empty;
            if (string.IsNullOrEmpty(player))
            {
                await cacheService.ListRightPushAsync(CacheKey.WaitingPlayer, account);

                var roomIdUsed = true;
                while (roomIdUsed)
                {
                    roomId = $"Room_{RandomNumberGenerator.GetHexString(16, true)}";
                    var cacheData = await cacheService.StringGetAsync(roomId);

                    roomIdUsed = !string.IsNullOrEmpty(cacheData);
                }

                await cacheService.StringSetAsync(roomId, account);

                return (false, roomId);
            }

            roomId = await cacheService.StringGetAsync(player.ToString());

            if (string.IsNullOrEmpty(roomId)) { throw new GameServiceException(ErrorCode.RoomNotFound); }

            var dto = new GameInfoDTO()
            {
                Player1Account = player.ToString(),
                Player2Account = account,
            };

            await BeginNewGame(dto);

            return (true, roomId);
        }

        //public async Task<string> HostGame()
        //{
        //    var roomId = string.Empty;
        //    var roomIdUsed = true;
        //    while (roomIdUsed)
        //    {
        //        roomId = $"Room_{RandomNumberGenerator.GetHexString(16, true)}";
        //        var cacheData = await cacheService.StringGetAsync(roomId);

        //        roomIdUsed = !string.IsNullOrEmpty(cacheData);
        //    }

        //    await cacheService.ListRightPushAsync(CacheKey.Room, roomId);

        //    return roomId;
        //}

        //public async Task<string> JoinGame()
        //{
        //    var cacheData = await cacheService.HashGetAsync(CacheKey.Room, );

        //    await BeginNewGame();

        //    return cacheData.ToString();
        //}

        //public async Task<bool> GetReady(string account)
        //{

        //}

        private async Task<string> BeginNewGame(GameInfoDTO dto)
        {
            await unitOfWork.BeginTransactionAsync();
            var currentGameId = RandomNumberGenerator.GetHexString(16, true);

            try
            {
                await ValidateGameInfo(dto);

                var entries = new HashEntry(currentGameId, JsonConvert.SerializeObject(dto.To<Game>()));
                await cacheService.HashSetAsync(CacheKey.CurrentGame, [entries]);

                return currentGameId;
            }
            catch (GameServiceException)
            {
                await cacheService.HashRemoveAsync(CacheKey.CurrentGame, currentGameId);
                throw;
            }
            catch (Exception)
            {
                await cacheService.HashRemoveAsync(CacheKey.CurrentGame, currentGameId);
                throw;
            }
        }

        public async Task BeginNewRound(RoundInfoDTO dto, string userAccount)
        {
            var cacheData = await cacheService.HashGetAsync(CacheKey.CurrentGame, dto.CurrentGameId);

            if(!cacheData.HasValue)
            {
                throw new GameServiceException(ErrorCode.GameNotExist);
            }

            var game = JsonConvert.DeserializeObject<GameDTO>(cacheData.ToString()) ?? throw new GameServiceException(ErrorCode.DeserializationFialed);

            dto.WhoGoesFirst = DetermineWhoGoesFirst();

            // If the second player is a bot, handle bot logic
            if (game.Player2Account == "Bot")
            {
                // Implement bot logic here
            }

            var entries = new HashEntry(game.CurrentGameId, JsonConvert.SerializeObject(dto.To<Game>()));
            await cacheService.HashSetAsync(CacheKey.CurrentGame, [entries]);
        }

        private static WhoGoesFirst DetermineWhoGoesFirst()
        {
            var random = RandomNumberGenerator.GetInt32(1);
            return (WhoGoesFirst)random;
        }

        public Func<PlayerRoundInfo, PlayerRoundInfo, Result> MapRule(Character character)
        {
            return character switch
            {
                Character.Assassin => new Assassin().Rule,
                Character.Deceiver => new Deceiver().Rule,
                Character.Knight => new Knight().Rule,
                Character.Lobbyist => new Lobbyist().Rule,
                Character.Lord => new Lord().Rule,
                Character.Soldier => new Soldier().Rule,
                _ => throw new NotImplementedException()
            };
        }

        private async Task ValidateGameInfo(GameInfoDTO dto)
        {
            _ = await unitOfWork.Members.GetByAccountAsync(dto.Player1Account) ?? throw new GameServiceException("player1 account does not exist!");
            _ = await unitOfWork.Members.GetByAccountAsync(dto.Player1Account) ?? throw new GameServiceException("player2 account does not exist!");

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

            if (chosenCharacters1.Count != 3 || chosenCharacters2.Count != 3) { throw new GameServiceException("Duplicated characters"); }
        }

        public async Task EndGame(string currentGameId, string userAccount)
        {
            var cacheData = await cacheService.HashGetAsync(CacheKey.CurrentGame, currentGameId);

            if (!cacheData.HasValue)
            {
                throw new GameServiceException(ErrorCode.GameNotExist);
            }

            var game = JsonConvert.DeserializeObject<GameDTO>(cacheData.ToString()) ?? throw new GameServiceException(ErrorCode.DeserializationFialed);

            // Update the game status in the database
            await unitOfWork.Games.UpdateAsync(game.To<Game>());
            await unitOfWork.CommitTransactionAsync();

            // Remove the game from Redis
            await cacheService.RemoveDataAsync(userAccount);
        }

        public async Task<Card> OpenNextCard(OpenNextCardRequestDTO dto)
        {
            // Retrieve the game from Redis
            var cacheData = await cacheService.HashGetAsync(CacheKey.CurrentGame, dto.CurrentGameId);

            if (!cacheData.HasValue)
            {
                throw new GameServiceException(ErrorCode.GameNotExist);
            }

            var game = JsonConvert.DeserializeObject<GameDTO>(cacheData.ToString()) ?? throw new GameServiceException(ErrorCode.DeserializationFialed);

            // Implement logic to open the next card
            int lastOpened = game.Player1Account == dto.UserAccount ?
                            ++game.Round[dto.RoundOrder].Player1.LastOpened :
                            ++game.Round[dto.RoundOrder].Player2.LastOpened;

            // Update the game state in Redis
            var entries = new HashEntry(game.CurrentGameId, JsonConvert.SerializeObject(dto.To<Game>()));
            await cacheService.HashSetAsync(CacheKey.CurrentGame, [entries]);

            return game.Round[dto.RoundOrder].Player2.ChosenCards[lastOpened];
        }

        public async Task EndRound(Character playerChosenCharacter, string userAccount)
        {
            // Retrieve the round information from Redis
            //var cacheData = await cacheService.HashGetAsync(CacheKey.CurrentGame, dto.CurrentGameId);

            //if (!cacheData.HasValue)
            //{
            //    throw new GameServiceException(ErrorCode.GameNotExist);
            //}

            //var game = JsonConvert.DeserializeObject<GameDTO>(cacheData.ToString()) ?? throw new GameServiceException(ErrorCode.DeserializationFialed);

            // Implement logic to end the round
            //round.Status = RoundStatus.Ended;
            //round.PlayerChosenCharacter = playerChosenCharacter;

            //// Update the round state in Redis
            //await cacheService.SetDataAsync(userAccount + "_round", round);
        }
    }

    public class GameServiceException(string message) : Exception(message)
    {
    }
}
