#nullable disable

namespace DAL
{
    public partial class Game
    {
        public Game()
        {
            Sessions = new HashSet<Session>();
        }
        public int Id { get; set; }
        public int HostId { get; set; }
        public bool Status { get; set; }

        public virtual Player Host { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
