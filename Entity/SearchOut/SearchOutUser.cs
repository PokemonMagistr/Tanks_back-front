using System;
using System.Collections.Generic;


namespace Entity.SearchOut
{
    public class SearchOutUser
    {
        public int Count { get; set; }
        public int Total { get; set; }
        public List<Entity.Player> Users { get; set; }
    }
}
