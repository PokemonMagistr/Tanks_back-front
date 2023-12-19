using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Objects
{
    public  class BlockObject : BaseObject
    {
        public BlockObject() : base()
        {
            Width = 30;
            Height = 30;
            Left = 510;
            Bottom = 510;
            BackgroundImage = "url(/im/block.png)";
            BackgroundSize = "contain";
            Position = "absolute";
            Type = "Block";
            
        }
    }
}
