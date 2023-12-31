﻿using Microsoft.AspNetCore.Mvc;
using Quartz;
using Quartz.Impl;
using System.Collections.Concurrent;
using System.Text.Json;

namespace game.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }


        public IActionResult ListGame()
        {
            var query = new Entity.SearchIn.SearchingGame();
            return View(BL.GameBL.Get(query));
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangeSkin()
        {
            return View();
        }
        public IActionResult ChangeTeam(int skinId)
        {
            return View(skinId);
        }


        ConcurrentDictionary<int, IScheduler> gameFlows = new ConcurrentDictionary<int, IScheduler>();

        StdSchedulerFactory factory = new StdSchedulerFactory();

        //[Authorize]
        public async Task<IActionResult> CreateGame()
        {

            var game = new Entity.Game();

            game.HostId = BL.PlayerBL.Get(User.Identity.Name).Id;
            game.Status = true;

            game.Id = BL.GameBL.AddGame(game);


            var hostSession = new Entity.Session();
            hostSession.UserId = game.HostId;
            hostSession.Status = game.Status;
            hostSession.GameId = game.Id;
            BL.SessionBL.AddOrUpdate(hostSession);

            BL.GameLogic.Initialization(game);


            ITrigger trigger = TriggerBuilder.Create()      // создаем триггер
            .WithIdentity(game.Id.ToString(), "group1")     // идентифицируем триггер с именем и группой
            .StartNow()                                     // запуск сразу после начала выполнения
            .WithSimpleSchedule(x => x                      // настраиваем выполнение действия
                .WithInterval(new TimeSpan(0, 0, 0, 0, 60)) // каждые 30 мс
                .RepeatForever())                           // бесконечное повторение
            .Build();

            IJobDetail job = JobBuilder.Create<BL.GamePlay>()
                .UsingJobData("gameId", game.Id)
                .Build();


            gameFlows.TryAdd(game.Id, await factory.GetScheduler());
            //IScheduler play = await factory.GetScheduler();



            //await play.Start();
            await gameFlows[game.Id].Start();

            //await play.ScheduleJob(job, trigger);
            await gameFlows[game.Id].ScheduleJob(job, trigger);



            BL.GameLogic.JoinUser(game.Id, game.HostId);

            return RedirectToAction("Play", "Game", new { gameId = game.Id, userId = game.HostId });
        }


        public IActionResult Join(int gameId)
        {
            var session = new Entity.Session();
            session.UserId = BL.PlayerBL.Get(User.Identity.Name).Id;
            session.GameId = gameId;

            BL.GameLogic.JoinUser(session.GameId ?? 0, session.UserId);

            BL.SessionBL.AddOrUpdate(session);

            return RedirectToAction("Play", "Game", new { gameId = session.GameId, userId = session.UserId });
        }

        public IActionResult Play(int gameId, int userId)
        {
            return View();
        }

        public IActionResult PressButton(int buttonCode, bool isDown, int gameId, int userId)
        {
            if (isDown)
            {
                if (!BL.GameLogic.allButtons.ContainsKey(gameId)) BL.GameLogic.allButtons.TryAdd(gameId, new ConcurrentDictionary<int, ConcurrentDictionary<int, bool>>());
                if (!BL.GameLogic.allButtons[gameId].ContainsKey(userId)) BL.GameLogic.allButtons[gameId].TryAdd(userId, new ConcurrentDictionary<int, bool>());
                if (!BL.GameLogic.allButtons[gameId][userId].ContainsKey(buttonCode))
                {

                    BL.GameLogic.allButtons[gameId][userId].TryAdd(buttonCode, true);


                }
            }
            else
            {
                if (BL.GameLogic.allButtons.ContainsKey(gameId) && BL.GameLogic.allButtons[gameId].ContainsKey(userId))
                {
                    if (BL.GameLogic.allButtons[gameId][userId].ContainsKey(buttonCode))
                    {
                        bool success;
                        BL.GameLogic.allButtons[gameId][userId].Remove(buttonCode, out success);
                    }
                }
            }
            return Ok(Json(true));
        }


        public IActionResult GetElements(int gameId, int userId, int WinWidth, int WinHeight)
        {

            if (BL.GameLogic.allGames.ContainsKey(gameId))
            {
                //var hero = (Entity.Objects.BaseObject)BL.GameLogic.allGames[gameId].FirstOrDefault(item => item.Value.Type == "hero" && ((Entity.Objects.BaseObject)item.Value).UserId == userId).Value;
                //if (hero == null)
                //{
                //    hero = new Entity.Objects.HeroObject();
                //}
                Entity.Objects.BaseObject[] temp = BL.GameLogic.allGames[gameId].Values
                .Select(item => (Entity.Objects.BaseObject)item.Clone())
                .ToArray();

                var json = JsonSerializer.Serialize((object[])temp);
                return Ok(json);
            }
            else return Ok();
        }
        public IActionResult GetPlayers(int gameId)
        {
            List<Entity.Player> users = new List<Entity.Player>();
            foreach (var i in BL.GameLogic.usersInGame[gameId])
            {
                users.Add(BL.PlayerBL.Get(i.Key));
            }

            var playersInJson = JsonSerializer.Serialize(users);
            return Ok(playersInJson);
        }
        public IActionResult GetPlayers2(int gameId)
        {
            //List<Entity.Player> users = new List<Entity.Player>();
            //foreach (var i in BL.GameLogic.usersInGame[gameId])
            //{
            //    users.Add(BL.PlayerBL.Get(i.Key));
            //}

            List<Entity.Objects.BaseObject> players = new List<Entity.Objects.BaseObject>();
            foreach (var i in BL.GameLogic.allGames[gameId].Values)
            {
                if (i.Type == "hero")
                {
                    players.Add(i);
                }
            }
            var playersInJson2 = JsonSerializer.Serialize(players);

            //var playersInJson = JsonSerializer.Serialize(users);
            return Ok(playersInJson2);
        }
        public async Task<IActionResult> Win(int userId)
        {
            var player = BL.PlayerBL.Get(userId);
            player.Score += 100;
            BL.PlayerBL.AddOrUpdate(player);
            return View(player);
        }
        public async Task<IActionResult> Lose(int userId)
        {
            var player = BL.PlayerBL.Get(userId);
            return View(player);
        }
    }
}
