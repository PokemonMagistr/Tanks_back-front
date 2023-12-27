namespace Entity.Objects
{
    public class HeroObject : BaseObject
    {


        public HeroObject(string color) : base()
        {
            if(color == "red")
            {
                Mess = "";
                Width = 30;
                Height = 30;
                Left = 910;
                Bottom = 150;
                BackgroundImage = "url(/im/red-tank.png)";
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
                ZIndex = 5;
                Opacity = 1;
                Color = "red";
                Dies = 0;
            }
            else if(color == "blue")
            {
                Mess = "";
                Width = 30;
                Height = 30;
                Left = 910;
                Bottom = 150;
                BackgroundImage = "url(/im/blue-tank.png)";
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
                ZIndex = 5;
                Opacity = 1;
                Color = "blue";
                Dies = 0;
            }
            

        }
    }


}