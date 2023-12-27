namespace Entity.Objects
{
    public class KustObject : BaseObject
    {
        public KustObject() : base()
        {
            Width = 30;
            Height = 30;
            Left = 510;
            Bottom = 510;
            BackgroundImage = "url(/im/Kust.png)";
            BackgroundSize = "contain";
            Position = "absolute";
            Type = "Kust";
            Destroy = false;
            ZIndex = 2;
            Opacity = 0.7;
        }
    }
}
