using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using System.Diagnostics.SymbolStore;
using System.Configuration;
using System.Reflection.PortableExecutable;
using Quartz.Util;

namespace BL
{
    public static class GameLogic
    {
        public static ConcurrentDictionary<int, ConcurrentDictionary<string, Entity.Objects.BaseObject>> allGames = new ConcurrentDictionary<int, ConcurrentDictionary<string, Entity.Objects.BaseObject>>();


        public static ConcurrentDictionary<int, ConcurrentDictionary<int, ConcurrentDictionary<int, bool>>> allButtons = new ConcurrentDictionary<int, ConcurrentDictionary<int, ConcurrentDictionary<int, bool>>>();

        public static ConcurrentDictionary<int, ConcurrentDictionary<int, bool>> usersInGame = new ConcurrentDictionary<int, ConcurrentDictionary<int, bool>>();

        //public static ConcurrentDictionary<int, ConcurrentDictionary<string, ConcurrentDictionary<string, string>>> Messages = new  ConcurrentDictionary<int, ConcurrentDictionary<string, ConcurrentDictionary<string, string>>>();
        public static ConcurrentDictionary<int, ConcurrentDictionary<string, Entity.Objects.BaseObject>> arrKusts = new ConcurrentDictionary<int, ConcurrentDictionary<string, Entity.Objects.BaseObject>>();
       
        //public static Entity.Objects.BaseObject temp;
        public static Entity.Objects.BaseObject obj;
        public static void Initialization(Entity.Game game)
        {

            allButtons.TryAdd(game.Id, new ConcurrentDictionary<int, ConcurrentDictionary<int, bool>>());
            usersInGame.TryAdd(game.Id, new ConcurrentDictionary<int, bool>());
            //Messages.TryAdd(game.Id,  new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>());
            //arrKusts.TryAdd(game.Id, new ConcurrentDictionary<string, Entity.Objects.BaseObject>());

            var arr = new ConcurrentDictionary<string, Entity.Objects.BaseObject>();
            var arrKusts2 = new ConcurrentDictionary<string, Entity.Objects.BaseObject>();
            #region Внешние границы
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
            #endregion

            #region Боковые палки
            for (int i = 0; i < 4; i++) // левая верхняя палка
            {
                var block = new Entity.Objects.BlockObject();
                block.Left = 500;
                block.Bottom = 450 + (i * 30);
                arr.TryAdd(block.Id, block);
            }
            var block22 = new Entity.Objects.BlockObject();
            block22.Left = 470;
            block22.Bottom = 500;
            arr.TryAdd(block22.Id, block22);

            for (int i = 0; i < 4; i++)// правая верхняя палка
            {
                var block = new Entity.Objects.BlockObject();
                block.Left = 1000;
                block.Bottom = 450 + (i * 30);
                arr.TryAdd(block.Id, block);
            }
            var block33 = new Entity.Objects.BlockObject();
            block33.Left = 1030;
            block33.Bottom = 500;
            arr.TryAdd(block33.Id, block33);

            for (int i = 0; i < 4; i++)// левая нижняя палка
            {
                var block = new Entity.Objects.BlockObject();
                block.Left = 500;
                block.Bottom = 120 + (i * 30);
                arr.TryAdd(block.Id, block);
            }
            var block44 = new Entity.Objects.BlockObject();
            block44.Left = 470;
            block44.Bottom = 170;
            arr.TryAdd(block44.Id, block44);

            for (int i = 0; i < 4; i++)// правая нижняя палка
            {
                var block = new Entity.Objects.BlockObject();
                block.Left = 1000;
                block.Bottom = 120 + (i * 30);
                arr.TryAdd(block.Id, block);
            }

            var block55 = new Entity.Objects.BlockObject();
            block55.Left = 1030;
            block55.Bottom = 170;
            arr.TryAdd(block55.Id, block55);
            #endregion

            #region Защита звёзд
            for (int i=0; i<4; i++) // Левая защита звезды
            {
                var block = new Entity.Objects.BlockObject();
                block.Left = 40 + (i * 30);
                block.Bottom = 400;
                arr.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 3; i++)// Левая защита звезды
            {
                var block = new Entity.Objects.BlockObject();
                block.Left = 130;
                block.Bottom = 370 - (i*30);
                arr.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 4; i++)// Левая защита звезды
            {
                var block = new Entity.Objects.BlockObject();
                block.Left = 40 + (i * 30);
                block.Bottom = 300;
                arr.TryAdd(block.Id, block);
            }


            for (int i = 0; i < 4; i++) // Правая защита звезды
            {
                var block = new Entity.Objects.BlockObject();
                block.Left = 1450 - (i * 30);
                block.Bottom = 400;
                arr.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 3; i++)// Правая защита звезды
            {
                var block = new Entity.Objects.BlockObject();
                block.Left = 1360;
                block.Bottom = 370 - (i * 30);
                arr.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 4; i++)// Правая защита звезды
            {
                var block = new Entity.Objects.BlockObject();
                block.Left = 1450 - (i * 30);
                block.Bottom = 300;
                arr.TryAdd(block.Id, block);
            }
            #endregion

            #region центр
            for (int i=0; i<2; i++) // желекза по центру
            {
                var block = new Entity.Objects.NRBlockObject();
                block.Left = 750;
                block.Bottom = 360 - (i * 30);
                arr.TryAdd(block.Id, block);
            }

            for(int i=0; i<11; i++) // над жеелзкой
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 390;
                block.Left = 600 + (i * 30);
                arr.TryAdd(block.Id, block);
            }
            for(int i=0; i<11; i++) //под железкой
            {
                var block = new Entity.Objects.BlockObject();
                block.Left = 600 + (i * 30);
                block.Bottom = 300;
                arr.TryAdd(block.Id, block);
            }
            #endregion

            #region нижние полосы кирпич
            for (int i=0; i<6; i++) // нижние полосы
            {
                var block = new Entity.Objects.BlockObject();
                block.Left = 350 + (i * 30);
                block.Bottom = 30;
                var block2 = new Entity.Objects.BlockObject();
                block2.Bottom = 30;
                block2.Left = 1000 + (i*30);

                var block3 = new Entity.Objects.BlockObject();
                block3.Left = 350 + (i * 30);
                block3.Bottom = 630;
                var block4 = new Entity.Objects.BlockObject();
                block4.Bottom = 630;
                block4.Left = 1000 + (i * 30);

                arr.TryAdd(block.Id, block);
                arr.TryAdd(block2.Id, block2);
                arr.TryAdd(block3.Id, block3);
                arr.TryAdd(block4.Id, block4);
            }
            #endregion

            #region Флаги
            var red_flag = new Entity.Objects.FlagObject("red");
            red_flag.Left = 40;
            red_flag.Bottom = 330;

            var blue_flag = new Entity.Objects.FlagObject("blue");
            blue_flag.Left = 1390;
            blue_flag.Bottom = 330;

            arr.TryAdd(red_flag.Id, red_flag);
            arr.TryAdd(blue_flag.Id, blue_flag);
            #endregion

            #region кусты по центру
            for (int i=0; i<5; i++) // кусты по центру
            {
                var kust1 = new Entity.Objects.KustObject();
                kust1.Left = 600 + (i*30);
                kust1.Bottom = 330;
                var kust2 = new Entity.Objects.KustObject();
                kust2.Left = 600 + (i * 30);
                kust2.Bottom = 360;

                var kust3 = new Entity.Objects.KustObject();
                kust3.Left = 780 + (i*30);
                kust3.Bottom = 330;
                var kust4 = new Entity.Objects.KustObject();
                kust4.Left = 780 + (i * 30);
                kust4.Bottom = 360;
                arrKusts2.TryAdd(kust1.Id, kust1);
                arrKusts2.TryAdd(kust2.Id, kust2);
                arrKusts2.TryAdd(kust3.Id, kust3);
                arrKusts2.TryAdd(kust4.Id, kust4);
                arr.TryAdd(kust1.Id, kust1);
                arr.TryAdd(kust2.Id, kust2);
                arr.TryAdd(kust3.Id, kust3);
                arr.TryAdd(kust4.Id, kust4);
            }
            #endregion

            #region Респауны
            var respawn1 = new Entity.Objects.RespawnObject();
            respawn1.Left = 200;
            respawn1.Bottom = 345;
            arr.TryAdd(respawn1.Id, respawn1);

            var respawn2 = new Entity.Objects.RespawnObject();
            respawn2.Left = 1290;
            respawn2.Bottom = 345;
            arr.TryAdd(respawn2.Id, respawn2);
            #endregion

            #region НР по 3 блока с кустами
            for (int i=0; i<3; i++)
            {
                var block = new Entity.Objects.NRBlockObject();
                block.Left = 720 + (i*30);
                block.Bottom = 540 - (i*30);
                var block2 = new Entity.Objects.NRBlockObject();
                block2.Left = 780 - (i * 30);
                block2.Bottom = 210 - (i * 30);

                arr.TryAdd(block.Id, block);
                arr.TryAdd(block2.Id, block2);
            }
            #endregion

            #region У респауна прямоугольники
            for (int i=0; i<4; i++)
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 400 + (i * 30);
                block.Left = 250;
                arr.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 4; i++)
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 400 + (i * 30);
                block.Left = 280;
                arr.TryAdd(block.Id, block);
            }


            for (int i = 0; i < 4; i++)
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 490;
                block.Left = 220 - (i*30);
                arr.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 4; i++)
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 460;
                block.Left = 220 - (i * 30);
                arr.TryAdd(block.Id, block);
            }


            for (int i = 0; i < 4; i++)
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 300 - (i * 30);
                block.Left = 250;
                arr.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 4; i++)
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 300 - (i * 30);
                block.Left = 280;
                arr.TryAdd(block.Id, block);
            }


            for (int i = 0; i < 4; i++)
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 210;
                block.Left = 220 - (i * 30);
                arr.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 4; i++)
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 240;
                block.Left = 220 - (i * 30);
                arr.TryAdd(block.Id, block);
            }
            //////////////////////////////////
            for (int i=0; i<4; i++)
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 400 + (i * 30);
                block.Left = 1240;
                arr.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 4; i++)
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 400 + (i * 30);
                block.Left = 1210;
                arr.TryAdd(block.Id, block);
            }

            for (int i = 0; i < 4; i++)
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 490;
                block.Left = 1240 + (i * 30);
                arr.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 4; i++)
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 460;
                block.Left = 1240 + (i * 30);
                arr.TryAdd(block.Id, block);
            }


            for (int i = 0; i < 4; i++)
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 300 - (i * 30);
                block.Left = 1240;
                arr.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 4; i++)
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 300 - (i * 30);
                block.Left = 1210;
                arr.TryAdd(block.Id, block);
            }

            for (int i = 0; i < 4; i++)
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 210;
                block.Left = 1240 + (i * 30);
                arr.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 4; i++)
            {
                var block = new Entity.Objects.BlockObject();
                block.Bottom = 240;
                block.Left = 1240 + (i * 30);
                arr.TryAdd(block.Id, block);
            }


            #endregion

            #region КУСТЫ У РЕСПАУНОВ
            for (int i=0; i < 6; i++)
            {
                var block = new Entity.Objects.KustObject();
                block.Bottom = 430 - (i * 30);
                block.Left = 340;
                arrKusts2.TryAdd(block.Id, block);
                arr.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 4; i++)
            {
                var block = new Entity.Objects.KustObject();
                block.Bottom = 400 - (i * 30);
                block.Left = 370;
                arr.TryAdd(block.Id, block);
                arrKusts2.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 2; i++)
            {
                var block = new Entity.Objects.KustObject();
                block.Bottom = 370 - (i * 30);
                block.Left = 400;
                arr.TryAdd(block.Id, block);
                arrKusts2.TryAdd(block.Id, block);
            }


            for (int i = 0; i < 6; i++)
            {
                var block = new Entity.Objects.KustObject();
                block.Bottom = 430 - (i * 30);
                block.Left = 1150;
                arr.TryAdd(block.Id, block);
                arrKusts2.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 4; i++)
            {
                var block = new Entity.Objects.KustObject();
                block.Bottom = 400 - (i * 30);
                block.Left = 1120;
                arr.TryAdd(block.Id, block);
                arrKusts2.TryAdd(block.Id, block);
            }
            for (int i = 0; i < 2; i++)
            {
                var block = new Entity.Objects.KustObject();
                block.Bottom = 370 - (i * 30);
                block.Left = 1090;
                arr.TryAdd(block.Id, block);
                arrKusts2.TryAdd(block.Id, block); ;
            }
            #endregion

            #region Блоки у кустов

            for (int i=0; i<8; i++)
            {
                for(int j=0; j<3; j++)
                {
                    var block = new Entity.Objects.BlockObject();
                    block.Left = 70 + (i*30);
                    block.Bottom = 60 + (j*30);
                    arr.TryAdd(block.Id, block);

                    var block2 = new Entity.Objects.BlockObject();
                    block2.Left = 1420 - (i * 30);
                    block2.Bottom = 60 + (j * 30);
                    arr.TryAdd(block2.Id, block2);
                }
            }



            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    var block = new Entity.Objects.BlockObject();
                    block.Left = 70 + (i * 30);
                    block.Bottom = 540 + (j * 30);
                    arr.TryAdd(block.Id, block);

                    var block2 = new Entity.Objects.BlockObject();
                    block2.Left = 1420 - (i * 30);
                    block2.Bottom = 540 + (j * 30);
                    arr.TryAdd(block2.Id, block2);
                }
            }

            #endregion

            #region блоки у НР блоков
            for(int i=0; i<3; i++)
            {
                for(int j=0; j<4; j++)
                {
                    var block = new Entity.Objects.BlockObject();
                    block.Left = 660 - (i * 30);
                    block.Bottom = 540 - (j * 30);
                    arr.TryAdd(block.Id, block);

                    var block2 = new Entity.Objects.BlockObject();
                    block2.Left = 840 + (i * 30);
                    block2.Bottom = 540 - (j * 30);
                    arr.TryAdd(block2.Id, block2);


                    var block3 = new Entity.Objects.BlockObject();
                    block3.Left = 660 - (i * 30);
                    block3.Bottom = 210 - (j * 30);
                    arr.TryAdd(block3.Id, block3);

                    var block4 = new Entity.Objects.BlockObject();
                    block4.Left = 840 + (i * 30);
                    block4.Bottom = 210 - (j * 30);
                    arr.TryAdd(block4.Id, block4);
                }
            }
            #endregion



            arrKusts.TryAdd(game.Id, arrKusts2);
            allGames.TryAdd(game.Id, arr);
        }

        public static void JoinUser(int gameId, int userId)
        {

            allButtons[gameId].TryAdd(userId, new ConcurrentDictionary<int, bool>());
            usersInGame[gameId].TryAdd(userId, true);

            ///Messages[gameId].TryAdd(userId.ToString(), new ConcurrentDictionary<string, string>());

            var count = 0;
            foreach(var i in usersInGame[gameId].Values)
            {
                count++;
            }

            var userHero = new Entity.Objects.HeroObject();
            if(count == 1)
            {
                userHero.Left = 200;
                userHero.Bottom = 345;
            }
            else if(count ==2)
            {
                userHero.Left = 1290;
                userHero.Bottom = 345;
            }
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
            foreach (var userId in usersInGame[gameId].Keys)
            {

                foreach(var i in allGames[gameId].Values)
                {
                    if(i.UserId == userId)
                    {
                        if(i.XPos !=0)
                        {
                            var ax = i.Left + i.XDelta;
                            var ax1 = i.Left + i.XDelta + i.Width;
                            var ay = i.Bottom;
                            var ay1 = i.Bottom + i.Height;

                            //var count = 0;
                            //foreach (var kust in arrKusts[gameId].Values)
                            //{

                            //    double cx = kust.Left;
                            //    double cx1 = kust.Left + kust.Width;
                            //    double cy = kust.Bottom;
                            //    double cy1 = kust.Bottom + kust.Height;
                            //    if (Intersects(cx + 5, cx1 - 5 , cy + 5, cy1 - 5, ax - i.XDelta, ax1 - i.XDelta, ay, ay1) == true)
                            //    {
                            //        Console.WriteLine("ЕСТЬ ЕСТЬ ЕСТЬ ЕСТЬ");
                            //        count++;
                            //        i.Opacity = 0.3;
                            //    }
                            //}
                            //if (count == 0)
                            //{
                            //    i.Opacity = 1;
                            //}

                            bool canMove = true;
                            foreach(var k  in allGames[gameId].Values)
                            {
                                double cx = k.Left;
                                double cx1 = k.Left + k.Width;
                                double cy = k.Bottom;
                                double cy1 = k.Bottom + k.Height;                    
                                if (i.UserId == k.UserId || k.Type == "Respawn" || k.Type == "Kust") continue;
                                if (Intersects(cx,cx1,cy,cy1, ax,ax1,ay,ay1) == true)
                                {
                                    canMove = false;
                                    i.XDelta = 0;
                                    i.XPos = 0;
                                    break;
                                }
                            }
                            if(canMove && i.XPos != 0 && i.XPos < 32 && i.XPos > -32)
                            {
                                if (i.XPos != 0 && i.XPos < 32 && i.XPos > -32)
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
                                if (i.UserId == k.UserId || k.Type == "Respawn" || k.Type == "Kust") continue;
                                if (Intersects(cx, cx1, cy, cy1, ax, ax1, ay, ay1) == true)
                                {
                                    canMove = false;
                                    i.YDelta = 0;
                                    i.YPos = 0;
                                    break;
                                }
                            }
                            if(canMove && i.YPos != 0 && i.YPos < 32 && i.YPos > -32)
                            {
                                if (i.YPos != 0 && i.YPos < 32 && i.YPos > -32)
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
                            if (i.Id == sb.Id || sb.Type == "Kust" || sb.Type == "Respawn") continue;
                            
                            if (Intersects(cx, cx1, cy, cy1, ax, ax1, ay, ay1) == true)
                            {

                                if(sb.Type == "hero")
                                {
                                    sb.HP = sb.HP - 25;
                                    if(sb.HP <= 0)
                                    {
                                        sb.Mess = "You Lose";

                                        _ = allGames[gameId].Remove(sb.Id, out _);
                                    }
                                }
                                else if(sb.Destroy)
                                {
                                    sb.HP -= 25;
                                    if(sb.HP <=0)
                                    {
                                        obj = sb;
                                        _ = allGames[gameId].Remove(obj.Id, out _);
                                    }
                                }
                                canMoveb = false;
                                i.XDelta = 0;
                                i.XPos = 0;


                                obj = i;
                                _ = allGames[gameId].Remove(obj.Id, out _);

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
                                    _ = allGames[gameId].Remove(obj.Id, out _);
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
                            if (i.Id == sb.Id || sb.Type == "Kust" || sb.Type == "Respawn") continue;
                            if (Intersects(cx, cx1, cy, cy1, ax, ax1, ay, ay1) == true)
                            {
                                if (sb.Type == "hero")
                                {
                                    sb.HP = sb.HP - 25;
                                    if (sb.HP <= 0)
                                    {
                                        sb.Mess = "You Lose";

                                        _ = allGames[gameId].Remove(sb.Id, out _);
                                    }
                                }
                                else if (sb.Destroy)
                                {
                                    sb.HP -= 25;
                                    if (sb.HP <= 0)
                                    {
                                        obj = sb;
                                        _ = allGames[gameId].Remove(obj.Id, out _);
                                    }
                                }
                                canMoveb = false;
                                i.YDelta = 0;
                                i.YPos = 0;


                                obj = i;
                                _ = allGames[gameId].Remove(obj.Id, out _);

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

                                    obj = i;
                                    _ = allGames[gameId].Remove(obj.Id, out _);
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

                            }
                        }
                        //left
                        if (GameLogic.allButtons[gameId][userId].ContainsKey(37))
                        {

                            if (i.XPos == 0)
                            {
                                
                                //hero.Angle = 270;
                                i.XPos = -30;
                                i.XDelta = -6;
                                i.Transform = "rotate(" + i.Angle + "deg)";

                            }
                            
                        }
                        //right
                        if (GameLogic.allButtons[gameId][userId].ContainsKey(39))
                        {

                            if (i.XPos == 0)
                            {
                                //hero.Angle = 90;
                                i.XPos = 30;
                                i.XDelta = 6;

                            }
                        }
                        //up
                        if (GameLogic.allButtons[gameId][userId].ContainsKey(38))
                        {

                            if (i.YPos == 0)
                            {
                                //hero.Angle = 0;
                                i.YPos = 30;
                                i.YDelta = 6;
                                i.Transform = "rotate(" + i.Angle + "deg)";
                                
                            }
                        }
                        //down
                        if (GameLogic.allButtons[gameId][userId].ContainsKey(40))
                        {

                            if (i.YPos == 0)
                            {
                                //hero.Angle = 180;
                                i.YPos = -30;
                                i.YDelta = -6;
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
                                    bullet.YPos = 500;
                                    bullet.YDelta = 20;
                                }
                                else if (i.Angle == 180)
                                {
                                    bullet.Left = i.Left + i.Width/2;
                                    bullet.Bottom = i.Bottom - 10;
                                    bullet.YPos = -500;
                                    bullet.YDelta = -20;
                                }
                                else if (i.Angle == 90)
                                {
                                    bullet.Left = i.Left + i.Width + 10;
                                    bullet.Bottom = i.Bottom + i.Height/2;
                                    bullet.XPos = 500;
                                    bullet.XDelta = 20;
                                }
                                else if (i.Angle == 270)
                                {
                                    bullet.Left = i.Left - 10;
                                    bullet.Bottom = i.Bottom + i.Height/2;
                                    bullet.XPos = -500;
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