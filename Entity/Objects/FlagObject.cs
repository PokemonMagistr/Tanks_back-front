using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Objects
{
    public class FlagObject : BaseObject
    {
        public FlagObject(string color) : base()
        {
            if (color == "red")
            {
                Width = 90;
                Height = 68;
                BackgroundImage = "url(/im/red-star.jpg)";
                BackgroundSize = "contain";
                Position = "absolute";
                Type = "Flag";
                Destroy = true;
                HP = 25;
                ZIndex = 1;
            }
            else if(color == "blue")
            {
                Width = 90;
                Height = 68;
                BackgroundImage = "url(/im/blue-star.png)";
                BackgroundSize = "contain";
                Position = "absolute";
                Type = "Flag";
                Destroy = true;
                HP = 25;
                ZIndex = 1;
            }
        }
    }
}
