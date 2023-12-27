namespace BL
{
    static public class SessionBL
    {
        static public int AddOrUpdate(Entity.Session session)
        {
            return DAL.SessionDAL.AddOrUpdate(session);
        }

        static public Entity.Session Get(int id)
        {
            return DAL.SessionDAL.Get(id);
        }

        static public Entity.SearchOut.SearchOutSession Get(Entity.SearchIn.SearchingSession query)
        {
            return DAL.SessionDAL.Get(query);
        }
    }
}