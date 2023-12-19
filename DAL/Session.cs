using System;
using System.Collections.Generic;

#nullable disable

namespace DAL
{
    public partial class Session
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
        public int? GameId { get; set; }


        public virtual Player User { get; set; }

        // public virtual Game Game { get; set; }
    }
}
