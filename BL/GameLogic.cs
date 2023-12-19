using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using System.Diagnostics.SymbolStore;
using System.Configuration;

namespace BL
{
    public static class GameLogic
    {
        public static ConcurrentDictionary<int, ConcurrentDictionary<string, Entity.Objects.BaseObject>> allGames = new ConcurrentDictionary<int, ConcurrentDictionary<string, Entity.Objects.BaseObject>>();


        public static ConcurrentDictionary<int, ConcurrentDictionary<int, ConcurrentDictionary<int, bool>>> allButtons = new ConcurrentDictionary<int, ConcurrentDictionary<int, ConcurrentDictionary<int, bool>>>();

        public static ConcurrentDictionary<int, ConcurrentDictionary<int, bool>> usersInGame = new ConcurrentDictionary<int, ConcurrentDictionary<int, bool>>();

        //public static ConcurrentDictionary<int, ConcurrentDictionary<int, (bool, double, double)>> allBullets = new ConcurrentDictionary<int, ConcurrentDictionary<int, (bool, double, double)>>();
        public static Entity.Objects.BaseObject temp;
        public static Entity.Objects.BaseObject obj;
        public static void Initialization(Entity.Game game)
        {
            //Entity.Objects.BaseObject temp;
            allButtons.TryAdd(game.Id, new ConcurrentDictionary<int, ConcurrentDictionary<int, bool>>());
            usersInGame.TryAdd(game.Id, new ConcurrentDictionary<int, bool>());
            //allBullets.TryAdd(game.Id, new ConcurrentDictionary<int, (bool, double, double)>());

            var arr = new ConcurrentDictionary<string, Entity.Objects.BaseObject>();
            //var array = new ConcurrentDictionary<string, Entity.Objects.Item>();


            for (int i = 0; i < 50; i++)// верхяя горизонтальная линия
            {
                var block = new Entity.Objects.WallObject();
                block.Left = 10 + (i * 30);
                block.Bottom = 660;
                arr.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 50; i++)//нижняя горизонтальная линия
            {
                var block = new Entity.Objects.WallObject();
                block.Left = 10 + (i * 30);
                block.Bottom = 0;
                arr.TryAdd(block.Id, block);
            }

            for (int i = 0; i < 21; i++) //левая вертикальная линия
            {
                var block = new Entity.Objects.WallObject();
                block.Left = 10;
                block.Bottom = 30 + (i * 30);
                arr.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 21; i++) //правая вертикальная линия
            {
                var block = new Entity.Objects.WallObject();
                block.Left = 1480;
                block.Bottom = 30 + (i * 30);
                arr.TryAdd(block.Id, block);
            }





            for (int i = 0; i < 4; i++) // левая верхняя палка
            {
                var block = new Entity.Objects.BlockObject();
                block.Left = 500;
                block.Bottom = 400 + (i * 30);
                arr.TryAdd(block.Id, block);
            }
            var block2 = new Entity.Objects.BlockObject();
            block2.Left = 470;
            block2.Bottom = 450;
            arr.TryAdd(block2.Id, block2);

            for (int i = 0; i < 4; i++)// правая верхняя палка
            {
                var block = new Entity.Objects.BlockObject();
                block.Left = 1000;
                block.Bottom = 400 + (i * 30);
                arr.TryAdd(block.Id, block);
            }
            var block3 = new Entity.Objects.BlockObject();
            block3.Left = 1030;
            block3.Bottom = 450;
            arr.TryAdd(block3.Id, block3);

            for (int i = 0; i < 4; i++)// левая нижняя палка
            {
                var block = new Entity.Objects.BlockObject();
                block.Left = 500;
                block.Bottom = 100 + (i * 30);
                arr.TryAdd(block.Id, block);
            }
            var block4 = new Entity.Objects.BlockObject();
            block4.Left = 470;
            block4.Bottom = 150;
            arr.TryAdd(block4.Id, block4);

            for (int i = 0; i < 4; i++)// правая нижняя палка
            {
                var block = new Entity.Objects.BlockObject();
                block.Left = 1000;
                block.Bottom = 100 + (i * 30);
                arr.TryAdd(block.Id, block);
            }
            var block5 = new Entity.Objects.BlockObject();
            block5.Left = 1030;
            block5.Bottom = 150;
            arr.TryAdd(block5.Id, block5);









            allGames.TryAdd(game.Id, arr);
        }

        public static void JoinUser(int gameId, int userId)
        {

            allButtons[gameId].TryAdd(userId, new ConcurrentDictionary<int, bool>());
            //allBullets[gameId].TryAdd(userId, (false, 0, 0));
            usersInGame[gameId].TryAdd(userId, true);


            var userHero = new Entity.Objects.HeroObject();
            userHero.UserId = userId;
            allGames[gameId].TryAdd(userHero.Id, userHero);

        }

        public static bool Intersects(double cx, double cx1, double cy, double cy1, double ax, double ax1, double ay, double ay1)
        {

            var bx = cx;
            var bx1 = cx1;
            var by = cy;
            var by1 = cy1;

            return (
                (
                    (
                        (ax >= bx && ax <= bx1) || (ax1 >= bx && ax1 <= bx1)
                    ) && (
                        (ay >= by && ay <= by1) || (ay1 >= by && ay1 <= by1)
                    )
                ) || (
                    (
                        (bx >= ax && bx <= ax1) || (bx1 >= ax && bx1 <= ax1)
                    ) && (
                        (by >= ay && by <= ay1) || (by1 >= ay && by1 <= ay1)
                    )
                )
            ) || (
                (
                    (
                        (ax >= bx && ax <= bx1) || (ax1 >= bx && ax1 <= bx1)
                    ) && (
                        (by >= ay && by <= ay1) || (by1 >= ay && by1 <= ay1)
                    )
                ) || (
                    (
                        (bx >= ax && bx <= ax1) || (bx1 >= ax && bx1 <= ax1)
                    ) && (
                        (ay >= by && ay <= by1) || (ay1 >= by && ay1 <= by1)
                    )
                )
            );
        }
        public static void MovePlayer(int gameId)
        {

            foreach(var userId in usersInGame[gameId].Keys)
            {
                foreach(var i in allGames[gameId].Values)
                {
                    //var hero = (Entity.Objects.BaseObject)i;
                    if(i.UserId == userId)
                    {
                        if(i.XPos !=0)
                        {

                            //hero.XPos = hero.XPos - hero.XDelta;
                            var ax = i.Left + i.XDelta;
                            var ax1 = i.Left + i.XDelta + i.Width;
                            var ay = i.Bottom;
                            var ay1 = i.Bottom + i.Height;
                            bool canMove = true;
                            foreach(var k  in allGames[gameId].Values)
                            {
                                double cx = k.Left;
                                double cx1 = k.Left + k.Width;
                                double cy = k.Bottom;
                                double cy1 = k.Bottom + k.Height;
                                    
                                if (i.UserId == k.UserId) continue;
                                if (Intersects(cx,cx1,cy,cy1, ax,ax1,ay,ay1) == true)
                                {
                                    canMove = false;
                                    i.XDelta = 0;
                                    i.XPos = 0;
                                    break;
                                }
                            }
                            if(canMove && i.XPos != 0 && i.XPos < 30 && i.XPos > -30)
                            {
                                if (i.XPos != 0 && i.XPos < 30 && i.XPos > -30)
                                {
                                    i.XPos = i.XPos - i.XDelta;
                                    i.Left = i.Left + i.XDelta;

                                }
                                else
                                {
                                    break;
                                }
                            }
                            
                        }
                        if(i.YPos !=0)
                        {
                            //hero.XPos = hero.XPos - hero.XDelta;
                            var ax = i.Left;
                            var ax1 = i.Left + i.Width;
                            var ay = i.Bottom + i.YDelta;
                            var ay1 = i.Bottom + i.YDelta + i.Height;
                            bool canMove = true;
                            foreach (var k in allGames[gameId].Values)
                            {
                                double cx = k.Left;
                                double cx1 = k.Left + k.Width;
                                double cy = k.Bottom;
                                double cy1 = k.Bottom + k.Height;
                                if (i.UserId == k.UserId) continue;
                                if (Intersects(cx, cx1, cy, cy1, ax, ax1, ay, ay1) == true)
                                {

                                    canMove = false;
                                    i.YDelta = 0;
                                    i.YPos = 0;
                                    break;
                                }
                            }
                            if(canMove && i.YPos != 0 && i.YPos < 30 && i.YPos > -30)
                            {
                                if (i.YPos != 0 && i.YPos < 30 && i.YPos > -30)
                                {
                                    i.YPos = i.YPos - i.YDelta;
                                    i.Bottom = i.Bottom + i.YDelta;
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                    }
                   
                }
            }
            

        }

        public static void MoveBullets(int gameId)
        {
            foreach(var i in allGames[gameId].Values)
            {
                if (i.Type == "bullet") //Дввижение пули по Х
                {

                    if (i.XPos != 0)
                    {
                        var ax = i.Left + i.XDelta;
                        var ax1 = i.Left + i.XDelta + i.Width;
                        var ay = i.Bottom;
                        var ay1 = i.Bottom + i.Height;
                        bool canMoveb = true;
                        foreach (var sb in allGames[gameId].Values)
                        {
                            double cx = sb.Left;
                            double cx1 = sb.Left + sb.Width;
                            double cy = sb.Bottom;
                            double cy1 = sb.Bottom + sb.Height;
                            if (i.Id == sb.Id) continue;
                            if (Intersects(cx, cx1, cy, cy1, ax, ax1, ay, ay1) == true)
                            {
                                canMoveb = false;
                                i.XDelta = 0;
                                i.XPos = 0;

                                //Entity.Objects.BaseObject temp;
                                //var obj = (Entity.Objects.BulletObject)i;
                                obj = i;
                                allGames[gameId].Remove(obj.Id, out temp);

                                break;
                            }
                        }
                        if (canMoveb)
                        {
                            if (i.XPos != 0)
                            {
                                i.XPos = i.XPos - i.XDelta;
                                i.Left = i.Left + i.XDelta;
                                if (i.XPos == 0)
                                {
                                    obj = i;
                                    allGames[gameId].Remove(obj.Id, out temp);
                                    break;
                                }
                            }
                            else if (i.XPos == 0)
                            {
                                
                            }
                        }
                    }
                    if (i.YPos != 0)//Дввижение пули по Y
                    {
                        var ax = i.Left;
                        var ax1 = i.Left + i.Width;
                        var ay = i.Bottom + i.YDelta;
                        var ay1 = i.Bottom + i.YDelta + i.Height;
                        bool canMoveb = true;
                        foreach (var sb in allGames[gameId].Values)
                        {
                            double cx = sb.Left;
                            double cx1 = sb.Left + sb.Width;
                            double cy = sb.Bottom;
                            double cy1 = sb.Bottom + sb.Height;
                            if (i.Id == sb.Id) continue;
                            if (Intersects(cx, cx1, cy, cy1, ax, ax1, ay, ay1) == true)
                            {
                                canMoveb = false;
                                i.YDelta = 0;
                                i.YPos = 0;

                                //Entity.Objects.BaseObject temp;
                                //var obj = (Entity.Objects.BulletObject)i;
                                obj = i;
                                allGames[gameId].Remove(obj.Id, out temp);

                                break;
                            }
                            
                        }
                        if (canMoveb)
                        {
                            if (i.YPos != 0)
                            {
                                i.YPos = i.YPos - i.YDelta;
                                i.Bottom = i.Bottom + i.YDelta;
                                if (i.YPos == 0)
                                {
                                    //Entity.Objects.BaseObject temp;
                                    //var obj = (Entity.Objects.BulletObject)i;
                                    obj = i;
                                    allGames[gameId].Remove(obj.Id, out temp);
                                    break;
                                }
                            }
                            else if (i.YPos == 0)
                            {

                            }
                        }
                    }
                }
            }         
            
        }

        
        public static void CheckMove(int gameId)
        {
            foreach(var userId in usersInGame[gameId].Keys)
            {
                foreach(var i in allGames[gameId].Values)
                {
                    if(i.UserId == userId)
                    {
                        var lastButtonEntry = allButtons[gameId][userId].LastOrDefault();
                        if(lastButtonEntry.Key != 0 && lastButtonEntry.Value == true)
                        {
                            switch (lastButtonEntry.Key)
                            {
                                case 37: // ArrowLeft
                                    i.Angle = 270;
                                    break;
                                case 38: // ArrowUp
                                    i.Angle = 0;
                                    break;
                                case 39: // ArrowRight
                                    i.Angle = 90;
                                    break;
                                case 40: // ArrowDown
                                    i.Angle = 180;
                                    break;
                                    // Добавьте обработку других клавиш при необходимости
                            }
                        }
                        //left
                        if (GameLogic.allButtons[gameId][userId].ContainsKey(37))
                        {

                            if (i.XPos == 0)
                            {
                                
                                //hero.Angle = 270;
                                i.XPos = -20;
                                i.XDelta = -5;
                                i.Transform = "rotate(" + i.Angle + "deg)";

                            }
                            
                        }
                        //right
                        if (GameLogic.allButtons[gameId][userId].ContainsKey(39))
                        {

                            if (i.XPos == 0)
                            {
                                //hero.Angle = 90;
                                i.XPos = 20;
                                i.XDelta = 5;

                            }
                        }
                        //up
                        if (GameLogic.allButtons[gameId][userId].ContainsKey(38))
                        {

                            if (i.YPos == 0)
                            {
                                //hero.Angle = 0;
                                i.YPos = 20;
                                i.YDelta = 5;
                                i.Transform = "rotate(" + i.Angle + "deg)";
                                
                            }
                        }
                        //down
                        if (GameLogic.allButtons[gameId][userId].ContainsKey(40))
                        {

                            if (i.YPos == 0)
                            {
                                //hero.Angle = 180;
                                i.YPos = -20;
                                i.YDelta = -5;
                                i.Transform = "rotate(" + i.Angle + "deg)";
                                
                            }
                        }
                        //пробел
                        if (GameLogic.allButtons[gameId][userId].ContainsKey(32))
                        {
                            if (!i.LastFire.HasValue || (DateTime.Now - i.LastFire.Value).TotalMilliseconds > 2000)
                            {

                                var bullet = new Entity.Objects.BulletObject();
                                i.LastFire = DateTime.Now;
                                if (i.Angle == 0)
                                {
                                    bullet.Left = i.Left + i.Width/2;
                                    bullet.Bottom = i.Bottom + i.Height + 10;
                                    bullet.YPos = 400;
                                    bullet.YDelta = 20;
                                }
                                else if (i.Angle == 180)
                                {
                                    bullet.Left = i.Left + i.Width/2;
                                    bullet.Bottom = i.Bottom - 10;
                                    bullet.YPos = -400;
                                    bullet.YDelta = -20;
                                }
                                else if (i.Angle == 90)
                                {
                                    bullet.Left = i.Left + i.Width + 10;
                                    bullet.Bottom = i.Bottom + i.Height/2;
                                    bullet.XPos = 400;
                                    bullet.XDelta = 20;
                                }
                                else if (i.Angle == 270)
                                {
                                    bullet.Left = i.Left - 10;
                                    bullet.Bottom = i.Bottom + i.Height/2;
                                    bullet.XPos = -400;
                                    bullet.XDelta = -20;
                                    
                                }
                                GameLogic.allGames[gameId].TryAdd(bullet.Id, bullet);
                            }
                        }
                    }
                }
            }
        }
    }
}