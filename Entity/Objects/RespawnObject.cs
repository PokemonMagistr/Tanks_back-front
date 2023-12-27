namespace Entity.Objects
{
    public class RespawnObject : BaseObject
    {
        public RespawnObject() : base()
        {
            Width = 30;
            Height = 30;
            BackgroundImage = "url(/im/respawn.png)";
            BackgroundSize = "contain";
            Position = "absolute";
            Type = "Respawn";
            Destroy = false;
            ZIndex = 2;
        }
    }
}
