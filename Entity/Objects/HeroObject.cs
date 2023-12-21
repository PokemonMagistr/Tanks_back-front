using System;
using System.Collections.Generic;

namespace Entity.Objects
{
    public class HeroObject : BaseObject
    {

        
        public HeroObject() : base()
        {
            Mess = "";
            Width = 40;
            Height = 40;
            Left = 910;
            Bottom = 150;
            BackgroundImage = "url(/im/player-top.png)";
            BackgroundSize = "contain";
            Position = "absolute";
            Type = "hero";
            Angle = 0;
            HP = 100;
            LastFire = null;
            //UserId = 0;
            XDelta = 0;
            YDelta = 0;
            XPos = 0;
            YPos = 0;

        }
    }

   
}