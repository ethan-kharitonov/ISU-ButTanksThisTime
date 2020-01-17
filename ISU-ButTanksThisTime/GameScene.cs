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
        private static StreamReader inFile;
        private static List<Vector2> pathPoints = new List<Vector2>();

        //Enemie Variables
        private static List<Tank> enemies = new List<Tank>();

        //Camera Variables
        private static Camera camera;

        static Tank TierTEnemy;
        static Tank TierREnemy;
        static Tank TierFEnemy;


        public static void LoadContent(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            camera = new Camera(GraphicsDevice.Viewport);
            Tools.Content = Content;

            //Load All Images//
            backgroundImg = Content.Load<Texture2D>("Images/Backgrounds/bg");
            bulletImg = Content.Load<Texture2D>("Images/Sprites/Bullets/Medium_Shell");
            barrelImg = Content.Load<Texture2D>("Images/Sprites/Terrain/Container_B");
            ///////////////////

            int arenaXPos = -(ARENA_WIDTH / 2) * backgroundImg.Width + Tools.screen.Center.X - backgroundImg.Width / 2;
            int arenaYPos = -ARENA_HEIGHT / 2 * backgroundImg.Height + Tools.screen.Center.Y - backgroundImg.Height / 2;
            int arenaWidth = ARENA_WIDTH * backgroundImg.Width;
            int arenaHeight = ARENA_HEIGHT * backgroundImg.Height;

            Tools.ArenaBounds = new Rectangle(arenaXPos, arenaYPos, arenaWidth, arenaHeight);

            player = new Player(Tools.screen.Center.ToVector2());
            barrelBox = new Rectangle(100, 100, barrelImg.Width, barrelImg.Height);
            LoadPath();

            TierTEnemy = new TierTwoEnemie(new Vector2(0, 0), 0, Stage.Low);
            TierREnemy = new TierThreeEnemie(new Vector2(100, 300), 0, Stage.Low);
            TierFEnemy = new TierFourEnemie(new Vector2(100, 500), 0, Stage.Low);
        }

        public static void Update()
        {
            player.Update(Vector2.Zero);
            camera.Update(player.GetPos());

            UpdateMines();
            UpdateEnemies();
            UpdateBullets();
            player.CollideWithObject(barrelBox);

        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transforme);

            for (int r = -ARENA_WIDTH / 2; r < ARENA_WIDTH / 2; ++r)
            {
                for (int c = -ARENA_HEIGHT / 2; c < ARENA_HEIGHT / 2; ++c)
                {
                    Rectangle bgBpx = new Rectangle(r * backgroundImg.Width + Tools.screen.Center.X, c * backgroundImg.Height + Tools.screen.Center.Y, backgroundImg.Width, backgroundImg.Height);
                    spriteBatch.Draw(backgroundImg, bgBpx, null, Color.White, 0, new Vector2(backgroundImg.Width / 2, backgroundImg.Height / 2), SpriteEffects.None, 1f);
                }
            }
            spriteBatch.Draw(barrelImg, barrelBox, Color.White);

            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }

            foreach(LandMine mine in landmines)
            {
                mine.Draw(spriteBatch);
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


            TierTEnemy.Draw(spriteBatch);
            TierREnemy.Draw(spriteBatch);
            TierFEnemy.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();
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

            inFile = File.OpenText(filePath);
            string[] data;

            while (!inFile.EndOfStream)
            {
                data = inFile.ReadLine().Split(',');
                pathPoints.Add(new Vector2((float)Convert.ToDouble(data[0]), (float)Convert.ToDouble(data[1])));
            }
            inFile.Close();
        }

        public static void AddBullet(Bullet bullet)
        {
            bullets.Add(bullet);
        }

        private static void UpdateMines()
        {
            //Create mines
            if (landMineTimer.IsTimeUp(Tools.gameTime) && Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                landMineTimer.Reset();
                landmines.Add(new RedMine(player.GetPos() - new Vector2(RedMine.Width/2.0f, RedMine.Height/2.0f)));
            }

            if (landMineTimer.IsTimeUp(Tools.gameTime) && Keyboard.GetState().IsKeyDown(Keys.E))
            {
                landMineTimer.Reset();
                landmines.Add(new BlueMine(player.GetPos() - new Vector2(RedMine.Width / 2.0f, RedMine.Height / 2.0f)));
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
            for(int i = 0; i < enemies.Count; ++i)
            {
                for (int k = 0; k < landmines.Count; k++)
                {
                    if (Tools.BoxBoxCollision(enemies[i].GetRotatedRectangle(), landmines[k].GetBox()) != null)
                    {
                        landmines[k].Collide();
                    }
                }
            }
        }

        private static void UpdateEnemies()
        {
            TierREnemy.Update(player.GetBasePosition());
            TierTEnemy.Update(Vector2.Zero);
            TierFEnemy.Update(player.GetBasePosition());

            //Spawn Enemies
            if (enemieTimer.IsTimeUp(Tools.gameTime) && enemies.Count < 20)
            {
                enemieTimer.Reset();
                enemies.Add(new TierOneEnemie(pathPoints[0], pathPoints, Stage.Low, 0));
            }

            if (bomberEnemieTimer.IsTimeUp(Tools.gameTime))
            {
                bomberEnemieTimer.Reset();
                float angle = MathHelper.ToRadians(Tools.rnd.Next(0, 361));
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
                        Vector2 newPos = (enemie1.GetPos() + enemie2.GetPos()) / 2;
                        Stage stage = (Stage)(Math.Max((int)enemie1.GetStage(), (int)enemie2.GetStage()) + 1);
                        stage = (Stage)Math.Min((int)stage, (int)(Stage.Player - 1));
                        enemies.Add(new TierOneEnemie(newPos, pathPoints, stage, enemie2.GetRotation()));
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
                    if (bullet.bulletOwner == Owner.Player && Tools.BoxBoxCollision(enemies[i].GetRotatedRectangle(), bullet.GetRotatedRectangle()) != null)
                    {
                        enemies[i].Collide(bullet);
                        bullet.Collide();
                    }
                }
            }

        }
    }
}
