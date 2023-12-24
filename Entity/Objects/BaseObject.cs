using System;
using System.Collections.Generic;

namespace Entity.Objects
{
    public class BaseObject : ICloneable
    {
        public string Id { get; set; }
        public int? UserId { get; set; }
        public double Left { get; set; }
        public double Bottom { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public string Transform { get; set; }
        public string BackgroundImage { get; set; }
        public string Position { get; set; }
        public string BackgroundSize { get; set; }

        public string Type { get; set; }

        public double XDelta { get; set; }
        public double YDelta { get; set; }
        public double XPos { get; set; }
        public double YPos { get; set; }

        public int Angle { get; set; }

        public int HP { get; set; }
        public DateTime? LastFire { get; set; }
        public string Mess { get; set; }

        public bool Destroy { get; set; }
        public int ZIndex { get; set; }

        public double Opacity { get; set; }
        private static int CurentId = 0;

        public BaseObject()
        {
            CurentId++;
            Id = CurentId.ToString();
        }

        public object Clone()
        {
            var clone = new BaseObject();
            clone.Id = this.Id;
            clone.Left = this.Left;
            clone.Bottom = this.Bottom;
            clone.Height = this.Height;
            clone.Width = this.Width;
            clone.Transform = this.Transform;
            clone.BackgroundImage = this.BackgroundImage;
            clone.Position = this.Position;
            clone.BackgroundSize = this.BackgroundSize;
            clone.Angle = this.Angle;
            clone.HP = this.HP;
            clone.Mess = this.Mess;

            clone.Type = this.Type;


            clone.XDelta = this.XDelta;
            clone.YDelta = this.YDelta;
            clone.XPos = this.XPos;
            clone.YPos = this.YPos;


            clone.UserId = this.UserId;

            clone.LastFire = this.LastFire;

            clone.Destroy = this.Destroy;

            clone.ZIndex = this.ZIndex;
            clone.Opacity = this.Opacity;
            return clone;
        }
    }
}