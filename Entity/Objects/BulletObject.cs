﻿namespace Entity.Objects
{
    public class BulletObject : BaseObject
    {
        public BulletObject() : base()
        {
            Width = 10;
            Height = 10;
            Left = 50;
            Bottom = 50;
            BackgroundImage = "url(/im/bullet.png)";
            BackgroundSize = "contatin";
            Position = "absolute";
            Type = "bullet";
            Destroy = false;
            XPos = 0;
            YPos = 0;
            XDelta = 0;
            YDelta = 0;

            ZIndex = 2;
        }
    }
}
