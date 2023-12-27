namespace DAL
{
    public class GameDAL
    {
        public static int AddOrUpdate(Entity.Game game)
        {
            using (HotlineContext data = new HotlineContext())
            {
                var dbGame = data.Games.FirstOrDefault(item => item.Id == game.Id);
                if (dbGame == null)
                {
                    dbGame = new Game();
                    data.Games.Add(dbGame);
                }
                dbGame.HostId = game.HostId;
                dbGame.Status = game.Status;
                data.SaveChanges();
                return dbGame.Id;
            }
        }
        public static Entity.Game Get(int ID)
        {
            using (HotlineContext data = new HotlineContext())
            {
                var dbGame = data.Games.FirstOrDefault(item => item.Id == ID);
                var entityGame = new Entity.Game();
                entityGame.Id = dbGame.Id;
                entityGame.HostId = dbGame.HostId;
                entityGame.Status = dbGame.Status;
                return entityGame;
            }
        }
        static public Entity.SearchOut.SearchOutGame Get(Entity.SearchIn.SearchingGame query)
        {
            var result = new Entity.SearchOut.SearchOutGame();

            using (HotlineContext data = new HotlineContext())
            {
                var temp = data.Games.Where(item => !query.Host.HasValue || item.Host.Equals(query.Host));
                result.Total = temp.Count();
                if (query.Top.HasValue)
                {
                    temp = temp.Take(query.Top.Value);
                }
                if (query.Skip.HasValue)
                {
                    temp = temp.Skip(query.Skip.Value);
                }
                result.Games = temp.Select(item => new Entity.Game()
                {
                    Id = item.Id,
                    HostId = item.HostId,
                    Status = item.Status,
                }).ToList();
            }

            return result;
        }
    }

}
