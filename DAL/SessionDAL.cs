namespace DAL
{
    public class SessionDAL
    {
        public static int AddOrUpdate(Entity.Session session)
        {
            using (HotlineContext data = new HotlineContext())
            {
                var dbSession = data.Sessions.FirstOrDefault(item => item.Id == session.Id);
                if (dbSession == null)
                {
                    dbSession = new Session();
                    data.Sessions.Add(dbSession);
                }
                dbSession.UserId = session.UserId;
                dbSession.Status = session.Status;
                dbSession.GameId = session.GameId;
                data.SaveChanges();
                return dbSession.Id;
            }
        }
        public static Entity.Session Get(int ID)
        {
            using (HotlineContext data = new HotlineContext())
            {
                var dbSession = data.Sessions.FirstOrDefault(item => item.Id == ID);
                var entitySession = new Entity.Session();
                entitySession.Id = dbSession.Id;
                entitySession.UserId = dbSession.UserId;
                entitySession.Status = dbSession.Status;
                entitySession.GameId = dbSession.GameId;
                return entitySession;
            }
        }
        static public Entity.SearchOut.SearchOutSession Get(Entity.SearchIn.SearchingSession query)
        {
            var result = new Entity.SearchOut.SearchOutSession();

            using (HotlineContext data = new HotlineContext())
            {
                var temp = data.Sessions.Where(item => item.GameId.Equals(query.Game)); // ?
                result.Total = temp.Count();
                if (query.Top.HasValue)
                {
                    temp = temp.Take(query.Top.Value);
                }
                if (query.Skip.HasValue)
                {
                    temp = temp.Skip(query.Skip.Value);
                }
                result.Sessions = temp.Select(item => new Entity.Session()
                {
                    Id = item.Id,
                    Status = item.Status,
                    UserId = item.UserId,
                    GameId = item.GameId,
                }).ToList();
            }

            return result;
        }
    }
}
