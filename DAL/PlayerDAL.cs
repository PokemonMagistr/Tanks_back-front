namespace DAL
{
    public class PlayerDAL
    {
        public static int AddOrUpdate(Entity.Player player)
        {
            using (HotlineContext data = new HotlineContext())
            {
                var dbPlayer = data.Players.FirstOrDefault(item => item.Id == player.Id);
                if (dbPlayer == null)
                {
                    dbPlayer = new Player();
                    data.Players.Add(dbPlayer);
                }
                dbPlayer.Name = player.Name;
                dbPlayer.Password = player.Password;
                dbPlayer.Score = player.Score;
                data.SaveChanges();
                return dbPlayer.Id;
            }
        }
        static public Entity.Player Get(int id)
        {
            using (HotlineContext data = new HotlineContext())
            {
                var databaseUser = data.Players.FirstOrDefault(x => x.Id == id);
                var user = new Entity.Player();
                user.Id = id;
                user.Name = databaseUser.Name;
                user.Password = databaseUser.Password;

                return user;
            }
        }
        public static Entity.Player Get(string login)
        {
            using (HotlineContext data = new HotlineContext())
            {
                var dbPlayer = data.Players.FirstOrDefault(item => item.Name == login);
                var entityPlayer = new Entity.Player();
                entityPlayer.Id = dbPlayer.Id;
                entityPlayer.Name = login;
                entityPlayer.Password = dbPlayer.Password;
                entityPlayer.Score = dbPlayer.Score;
                return entityPlayer;
            }
        }
        static public Entity.SearchOut.SearchOutUser Get(Entity.SearchIn.SearchingUser query)
        {
            var result = new Entity.SearchOut.SearchOutUser();

            using (HotlineContext data = new HotlineContext())
            {
                var temp = data.Players.Where(item => item.Name.StartsWith(query.Name));
                result.Total = temp.Count();
                if (query.Top.HasValue)
                {
                    temp = temp.Take(query.Top.Value);
                }
                if (query.Skip.HasValue)
                {
                    temp = temp.Skip(query.Skip.Value);
                }
                result.Users = temp.Select(item => new Entity.Player()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Password = item.Password,
                    Score = item.Score,
                }).ToList();
            }

            return result;
        }
    }
}
