using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class PlayerBL
    {
        public static int Registrations(Entity.Player player)
        {
            return DAL.PlayerDAL.AddOrUpdate(player);
        }

        public static bool Autorisations(Entity.Player player)
        {
            var getPlayer = DAL.PlayerDAL.Get(player.Name);
            if (player.Password == getPlayer.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public Entity.Player Get(int id)
        {
            return DAL.PlayerDAL.Get(id);
        }
        public static Entity.Player Get(string login)
        {
            return DAL.PlayerDAL.Get(login);
        }

        static public Entity.SearchOut.SearchOutUser Get(Entity.SearchIn.SearchingUser query)
        {
            return DAL.PlayerDAL.Get(query);
        }
    }
}
