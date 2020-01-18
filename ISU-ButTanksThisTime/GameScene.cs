using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    static class GameScene
    {
        //Background Variables
        private static Texture2D backgroundImg;
        private const int ARENA_WIDTH = 10;
        private const int ARENA_HEIGHT = 6;

        private static Player player;

        //Bullet Variables
        private static List<Bullet> bullets = new List<Bullet>();
        private static Texture2D bulletImg;
        private static Timer bulletTimer = new Timer(300);
        private static List<LandMine> landmines = new List<LandMine>();
        private static Timer landMineTimer = new Timer(2000);

        //Basic Enemie Variables
        private static Timer enemieTimer = new Timer(2000);
        private const int ENEMIE_SPAWN_DIS = 1000;

        //Bomber Enemie Variables
        private static Timer bomberEnemieTimer = new Timer(4000);

        //Barrel Variables
        private static Texture2D barrelImg;
        private static Rectangle barrelBox;


        //Load Path Variables
        private static List<Vector2>[] pathPoints = new List<Vector2>[3];


        //Enemie Variables
        private static List<Tank> enemies = new List<Tank>();

        //Camera Variables
        private static Camera camera;

        private static Tank[,] combos = new Tank[6, 6];

        private static Texture2D crossHairs;

        private static Timer freezeTimer = new Timer(4000);
        private static bool freeze = false;
        private static Texture2D frozenScreen;


        //Inventory variables
        private static List<Item> itemsOnMap = new List<Item>();
        private static Inventory inventory = new Inventory();

        public static void LoadContent()
        {
            camera = new Camera(Tools.Graphics.Viewport);

            //Load All Images//
            backgroundImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/bg");
            bulletImg = Tools.Content.Load<Texture2D>("Images/Sprites/Bullets/Medium_Shell");
            barrelImg = Tools.Content.Load<Texture2D>("Images/Sprites/Terrain/Container_B");
            crossHairs = Tools.Content.Load<Texture2D>("Images/Sprites/Crosshairs/crosshair068");
            ///////////////////

            int arenaXPos = -(ARENA_WIDTH / 2) * backgroundImg.Width + Tools.Screen.Center.X - backgroundImg.Width / 2;
            int arenaYPos = -ARENA_HEIGHT / 2 * backgroundImg.Height + Tools.Screen.Center.Y - backgroundImg.Height / 2;
            int arenaWidth = ARENA_WIDTH * backgroundImg.Width;
            int arenaHeight = ARENA_HEIGHT * backgroundImg.Height;

            Tools.ArenaBounds = new Rectangle(arenaXPos, arenaYPos, arenaWidth, arenaHeight);

            player = new Player(Tools.Screen.Center.ToVector2());
            LoadPath();
            barrelBox = new Rectangle(Tools.ArenaBounds.Left, Tools.ArenaBounds.Top, barrelImg.Width, barrelImg.Height);

            enemies.Add(new TierTwoEnemie(new Vector2(0, 0), 0, Stage.Low));
            enemies.Add(new BurstEnemie(new Vector2(100, 300), 0, Stage.Low));
            enemies.Add(new TierFourEnemie(new Vector2(100, 500), 0, Stage.Low, pathPoints[1]));

            for(int i = 0; i < 5; ++i)
            {
                for(int k = 0; k < 5; ++k)
                {
                    combos[i, k] = null;
                }
            }

            combos[(int)TankType.BasicPath, (int)TankType.Bomber] = new HealerEnemy(Vector2.Zero, 0, Stage.Low, pathPoints[2]);
            combos[(int)TankType.BasicPath, (int)TankType.RotateShooter] = new BurstEnemie(Vector2.Zero, 0, Stage.Low);

            combos[(int)TankType.Bomber, (int)TankType.BasicPath] = new HealerEnemy(Vector2.Zero, 0, Stage.Low, pathPoints[2]);
            combos[(int)TankType.Bomber, (int)TankType.RotateShooter] = new TierTwoEnemie(Vector2.Zero, 0, Stage.Low);

            combos[(int)TankType.RotateShooter, (int)TankType.BasicPath] = new BurstEnemie(Vector2.Zero, 0, Stage.Low);
            combos[(int)TankType.RotateShooter, (int)TankType.Bomber] = new TierTwoEnemie(Vector2.Zero, 0, Stage.Low);

            frozenScreen = new Texture2D(Tools.Graphics, Tools.ArenaBounds.Width, Tools.ArenaBounds.Height);

            Color[] data = new Color[Tools.ArenaBounds.Width * Tools.ArenaBounds.Height];
            for (int i = 0; i < data.Length; ++i) 
            {
                data[i] = Color.LightGray;
            } 
            frozenScreen.SetData(data);

        }

        public static void Update()
        {
            camera.Update(player.GetPos());

            Vector2 ScreenTL = player.GetPos() - Tools.Screen.Size.ToVector2() / 2;
            ScreenTL.X = MathHelper.Clamp(ScreenTL.X, Tools.ArenaBounds.Left, Tools.ArenaBounds.Right - Tools.Screen.Width / 2f);
            ScreenTL.Y = MathHelper.Clamp(ScreenTL.Y, Tools.ArenaBounds.Top, Tools.ArenaBounds.Bottom - Tools.Screen.Height / 2);
            Tools.TrueMousePos = Mouse.GetState().Position.ToVector2() + ScreenTL;

            inventory.Update();
            player.Update(Vector2.Zero);
            freeze = !freezeTimer.IsTimeUp(Tools.GameTime);
            if (!freeze)
            {
                UpdateMines();
                UpdateEnemies();
                UpdateBullets();
                UpdateItems();
            }


        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transforme);

            for (int r = -ARENA_WIDTH / 2; r < ARENA_WIDTH / 2; ++r)
            {
                for (int c = -ARENA_HEIGHT / 2; c < ARENA_HEIGHT / 2; ++c)
                {
                    Rectangle bgBpx = new Rectangle(r * backgroundImg.Width + Tools.Screen.Center.X, c * backgroundImg.Height + Tools.Screen.Center.Y, backgroundImg.Width, backgroundImg.Height);
                    spriteBatch.Draw(backgroundImg, bgBpx, null, Color.White, 0, new Vector2(backgroundImg.Width / 2, backgroundImg.Height / 2), SpriteEffects.None, 1f);
                }
            }


            foreach(LandMine mine in landmines)
            {
                mine.Draw(spriteBatch);
            }

            foreach(Item item in itemsOnMap)
            {
                item.Draw(spriteBatch);
            }

            foreach (Tank enemie in enemies)
            {
                enemie.Draw(spriteBatch);
            }

            RotatedRectangle obsticalBox = new RotatedRectangle(barrelBox, 0, Vector2.Zero);

            spriteBatch.Draw(Tools.RedSquare, obsticalBox.TopLeft, Color.White);
            spriteBatch.Draw(Tools.RedSquare, obsticalBox.TopRight, Color.White);
            spriteBatch.Draw(Tools.RedSquare, obsticalBox.BotomLeft, Color.White);
            spriteBatch.Draw(Tools.RedSquare, obsticalBox.BotomRight, Color.White);



            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }

            spriteBatch.Draw(barrelImg, barrelBox, null, Color.White, MathHelper.ToRadians(45), new Vector2(barrelImg.Width * 0.5f, barrelImg.Height * 0.5f), SpriteEffects.None, 1);
            
            if (freeze)
            {
                spriteBatch.Draw(frozenScreen, Tools.ArenaBounds, Color.DarkRed * 0.3f);
            }
            player.Draw(spriteBatch);
            spriteBatch.Draw(crossHairs, Tools.TrueMousePos, null, Color.White, 0, new Vector2(crossHairs.Width * 0.5f, crossHairs.Height * 0.5f), 0.25f, SpriteEffects.None, 10);
            spriteBatch.End();

            inventory.Draw(spriteBatch);
        }

        private static void LoadPath()
        {
            string filePath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            filePath = Path.GetDirectoryName(filePath);
            filePath = filePath.Substring(6);
            string foundPath = null;
            var found = false;
            while (filePath.Length > 3 && !(found = File.Exists(foundPath = filePath + "/saves/MyFile.txt")))
            {
                filePath = Path.GetDirectoryName(filePath);
            }
            if (!found)
            {
                return;
            }

            filePath = foundPath;

            var curPath = 0;
            List<Vector2> points = new List<Vector2>();
            using (var inFile = File.OpenText(filePath))
            {
                string[] data;
                while (!inFile.EndOfStream)
                {
                    string line;
                    while (!inFile.EndOfStream && (line = inFile.ReadLine()) != "#")
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        data = line.Split(',');
                        points.Add(new Vector2((float)Convert.ToDouble(data[0]), (float)Convert.ToDouble(data[1])));
                    }
                    pathPoints[curPath] = points;
                    points = new List<Vector2>();
                    ++curPath;
                }
            }
        }

        public static void AddBullet(Bullet bullet)
        {
            if (freeze)
            {
                return;
            }
            bullets.Add(bullet);
        }

        private static void UpdateMines()
        {
            //Create mines
            if (landMineTimer.IsTimeUp(Tools.GameTime) && Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                landMineTimer.Reset();
                landmines.Add(new RedMine(player.GetPos()));
            }

            if (landMineTimer.IsTimeUp(Tools.GameTime) && Keyboard.GetState().IsKeyDown(Keys.E))
            {
                landMineTimer.Reset();
                landmines.Add(new BlueMine(player.GetPos()));
            }


            //Update mines
            for (int i = 0; i < landmines.Count; ++i)
            {
                if (landmines[i].Update())
                {
                    landmines.RemoveAt(i);
                    --i;
                }
            }

            //Check collision
            for (int k = 0; k < landmines.Count; k++)
            {
                //check on player
                if(landmines[k] is RedMine)
                {
                    if (Tools.BoxBoxCollision(player.GetRotatedRectangle(), landmines[k].GetBox()) != null)
                    {
                        landmines[k].Collide();
                    }
                    continue;
                }

                //check on enemy;
                for (int i = 0; i < enemies.Count; ++i)
                {
                    if (Tools.BoxBoxCollision(enemies[i].GetRotatedRectangle(), landmines[k].GetBox()) != null)
                    {
                        landmines[k].Collide();
                    }

                    if (landmines[k].IsActive() && Tools.CirclePointCollision(landmines[k].GetExplosionArea(), enemies[i].GetPos()))
                    {
                        enemies[i].Collide(landmines[k]);
                    }
                }
            }
        }

        private static void UpdateEnemies()
        {
            //Spawn Enemies
            if (enemieTimer.IsTimeUp(Tools.GameTime) && enemies.Count < 20)
            {
                enemieTimer.Reset();
                enemies.Add(new TierOneEnemie(pathPoints[0][0], pathPoints[0], Stage.Low, 0));
            }

            if (bomberEnemieTimer.IsTimeUp(Tools.GameTime))
            {
                bomberEnemieTimer.Reset();
                float angle = MathHelper.ToRadians(Tools.Rnd.Next(0, 361));
                Vector2 pos = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * ENEMIE_SPAWN_DIS;
                pos = player.GetPos() + pos;

                enemies.Add(new BomberEnemie(pos, 0, Stage.Low));
            }

            //Update Enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].Update(player.GetPos()))
                {
                    enemies.RemoveAt(i);
                    --i;
                }
            }

            //Enemie to player collision
            for (int i = 0; i < enemies.Count; i++)
            {
                Tank enemie1 = enemies[i];
                if (Tools.BoxBoxCollision(enemie1.GetRotatedRectangle(), player.GetRotatedRectangle()) != null)
                {
                    enemie1.Collide(player);
                    player.Collide(enemie1);
                }
            }

            //Enemie to enemie collision
            for (int i = 0; i < enemies.Count; i++)
            {
                Tank enemie1 = enemies[i];
                for (int k = 0; k < enemies.Count; k++)
                {
                    Tank enemie2 = enemies[k];
                    if (Tools.BoxBoxCollision(enemie1.GetRotatedRectangle(), enemie2.GetRotatedRectangle()) != null && !enemie1.Equals(enemie2))
                    {
                        enemies.RemoveAt(i);
                        enemies.RemoveAt(k - 1);
                        --i;

                        if (enemie1.GetTankType() == enemie2.GetTankType())
                        {
                            enemies.Add(MergeTanks(enemie1, enemie2));
                        }
                        else
                        {
                            Vector2 newPos = (enemie1.GetPos() + enemie2.GetPos()) / 2;
                            Tank newEnemie = combos[(int)enemie1.GetTankType(), (int)enemie2.GetTankType()];
                            if (newEnemie == null)
                            {
                                enemies.Add(MergeTanks(enemie1, enemie2));
                            }
                            else
                            {
                                enemies.Add(newEnemie.Clone(newPos, enemie2.GetRotation(), Stage.Low));
                            }
                        }
                        break;
                    }
                }
            }

        }

        private static void UpdateBullets()
        {
            //Update bullets
            for (int i = 0; i < bullets.Count; ++i)
            {
                if (bullets[i].Update())
                {
                    bullets.RemoveAt(i);
                    --i;
                }
            }
            
            //Bullet to player collision
            foreach (Bullet bullet in bullets)
            {
                if (bullet.bulletOwner == Owner.Enemie && Tools.BoxBoxCollision(player.GetRotatedRectangle(), bullet.GetRotatedRectangle()) != null)
                {
                    //player.Collide(bullet);
                    bullet.Collide();
                }
            }

            //Bullet enemie collision
            for (int i = 0; i < enemies.Count; i++)
            {
                foreach (Bullet bullet in bullets)
                {
                    if (bullet.IsDead)
                    {
                        continue;
                    }
                    if (bullet.bulletOwner == Owner.Player && Tools.BoxBoxCollision(enemies[i].GetRotatedRectangle(), bullet.GetRotatedRectangle()) != null)
                    {
                        enemies[i].Collide(bullet);
                        bullet.Collide();
                    }
                }
            }

        }

        private static void UpdateItems()
        {
            for (int i = 0; i < itemsOnMap.Count; i++)
            {
                Item item = itemsOnMap[i];
                if (Tools.BoxBoxCollision(item.Box, player.GetRotatedRectangle()) != null)
                {
                    itemsOnMap.RemoveAt(i);
                    --i;
                    inventory.AddItem(item);
                }
            }
        }

        public static void AddLandMine(LandMine mine)
        {
            landmines.Add(mine);
        }

        private static Tank MergeTanks(Tank tank1, Tank tank2)
        {
            Vector2 newPos = (tank1.GetPos() + tank2.GetPos()) / 2;
            Stage stage = (Stage)(Math.Max((int)tank1.GetStage(), (int)tank2.GetStage()) + 1);
            stage = (Stage)Math.Min((int)stage, (int)(Stage.Player - 1));

            return tank1.Clone(newPos, tank2.GetRotation(), stage);
        }

        public static void AddItem(Item item)
        {
            itemsOnMap.Add(item);
        }

        public static void FreezeGame()
        {
            freezeTimer.Reset();
        }
    }
}
