// ***********************************************************************
// Assembly         : ISU-ButTanksThisTime
// Author           : Ethan Kharitonov
// Created          : 01-16-2020
//
// Last Modified By : Ethan Kharitonov
// Last Modified On : 01-19-2020
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using ISU_ButTanksThisTime.Bullets;
using ISU_ButTanksThisTime.Cannons;
using ISU_ButTanksThisTime.Collectibles;
using ISU_ButTanksThisTime.LandMines;
using ISU_ButTanksThisTime.Shapes;
using ISU_ButTanksThisTime.Tanks;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Class GameScene.
    /// </summary>
    internal static class GameScene
    {
        //Background Variables
        private static Texture2D backgroundImg;
        private const int ARENA_WIDTH = 10;
        private const int ARENA_HEIGHT = 6;

        private static Player player;

        //Bullet Variables
        private static readonly List<Bullet> bullets = new List<Bullet>();
        private static readonly List<LandMine> landmines = new List<LandMine>();
        private static readonly Timer landMineTimer = new Timer(2000);

        //Basic Enemy Variables
        private static readonly Timer enemyTimer = new Timer(2000);
        private const int ENEMY_SPAWN_DIS = 1000;

        //Bomber Enemy Variables
        private static readonly Timer bomberEnemyTimer = new Timer(4000);

        //Barrel Variables
        private static Texture2D barrelImg;
        private static Rectangle barrelBox;


        //Load Path Variables
        private static readonly List<Vector2>[] pathPoints = new List<Vector2>[3];


        //Enemy Variables
        private static readonly List<Tank> enemies = new List<Tank>();
        private static Texture2D enemyBaseImg;


        //Camera Variables
        private static Camera camera;

        private static readonly Tank[,] combos = new Tank[6, 6];

        private static Texture2D crossHairs;

        private static readonly Timer freezeTimer = new Timer(4000);
        private static bool freeze;
        private static Texture2D frozenScreen;


        //Inventory variables
        private static readonly List<Item> itemsOnMap = new List<Item>();
        private static Inventory inventory = new Inventory();


        private static Button pauseBtn;

        /// <summary>
        /// Loads the content.
        /// </summary>
        public static void LoadContent()
        {
            camera = new Camera(Tools.Graphics.Viewport);

            //Load All Images//
            backgroundImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/bg");
            Tools.Content.Load<Texture2D>("Images/Sprites/Bullets/Medium_Shell");
            barrelImg = Tools.Content.Load<Texture2D>("Images/Sprites/Terrain/Container_B");
            crossHairs = Tools.Content.Load<Texture2D>("Images/Sprites/Crosshairs/crosshair068");
            enemyBaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Terrain/Container_D");
            ///////////////////

            pauseBtn = new Button(Tools.ButtonImg, new Rectangle(15, 15, 100, 50), "PAUSE");

            var arenaXPos = -(ARENA_WIDTH / 2) * backgroundImg.Width + Tools.Screen.Center.X - backgroundImg.Width / 2;
            var arenaYPos = -ARENA_HEIGHT / 2 * backgroundImg.Height + Tools.Screen.Center.Y - backgroundImg.Height / 2;
            var arenaWidth = ARENA_WIDTH * backgroundImg.Width;
            var arenaHeight = ARENA_HEIGHT * backgroundImg.Height;

            Tools.ArenaBounds = new Rectangle(arenaXPos, arenaYPos, arenaWidth, arenaHeight);

            player = new Player(Tools.Screen.Center.ToVector2());
            LoadPath();
            barrelBox = new Rectangle(Tools.ArenaBounds.Left, Tools.ArenaBounds.Top, barrelImg.Width, barrelImg.Height * 2);


            for (var i = 0; i < 5; ++i)
            for (var k = 0; k < 5; ++k)
            {
                combos[i, k] = null;
            }

            combos[(int) TankType.BasicPath, (int) TankType.Bomber] = new HealerEnemy(Vector2.Zero, 0, Stage.Low, pathPoints[2]);
            combos[(int) TankType.BasicPath, (int) TankType.RotateShooter] = new BurstEnemy(Vector2.Zero, 0, Stage.Low);

            combos[(int) TankType.Bomber, (int) TankType.BasicPath] = new HealerEnemy(Vector2.Zero, 0, Stage.Low, pathPoints[2]);
            combos[(int) TankType.Bomber, (int) TankType.RotateShooter] = new TierTwoEnemy(Vector2.Zero, 0, Stage.Low);

            combos[(int) TankType.RotateShooter, (int) TankType.BasicPath] = new BurstEnemy(Vector2.Zero, 0, Stage.Low);
            combos[(int) TankType.RotateShooter, (int) TankType.Bomber] = new TierTwoEnemy(Vector2.Zero, 0, Stage.Low);

            frozenScreen = new Texture2D(Tools.Graphics, Tools.ArenaBounds.Width, Tools.ArenaBounds.Height);

            var data = new Color[Tools.ArenaBounds.Width * Tools.ArenaBounds.Height];
            for (var i = 0; i < data.Length; ++i)
            {
                data[i] = Color.LightGray;
            }

            frozenScreen.SetData(data);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public static void Update()
        {
            camera.Update(player.GetPos());

            var screenTl = player.GetPos() - Tools.Screen.Size.ToVector2() / 2;
            screenTl.X = MathHelper.Clamp(screenTl.X, Tools.ArenaBounds.Left, Tools.ArenaBounds.Right - Tools.Screen.Width / 2f);
            screenTl.Y = MathHelper.Clamp(screenTl.Y, Tools.ArenaBounds.Top, Tools.ArenaBounds.Bottom - Tools.Screen.Height / 2);
            Tools.TrueMousePos = Mouse.GetState().Position.ToVector2() + screenTl;

            if (pauseBtn.Update())
            {
                Game1.State = State.Pause;
            }

            inventory.Update();
            player.Update(Vector2.Zero);

            var trueBarrelBox = barrelBox;
            trueBarrelBox.Height = (int) (trueBarrelBox.Height * 0.60);
            if (Tools.BoxBoxCollision(player.GetRotatedRectangle(), new RotatedRectangle(trueBarrelBox, 45, new Vector2(barrelBox.Width / 2f, barrelBox.Height / 2f))))
            {
                Game1.State = State.Shop;
            }

            freeze = !freezeTimer.IsTimeUp(Tools.GameTime);
            if (freeze)
            {
                return;
            }

            UpdateMines();
            UpdateEnemies();
            UpdateBullets();
            UpdateItems();
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            for (var r = -ARENA_WIDTH / 2; r < ARENA_WIDTH / 2; ++r)
            for (var c = -ARENA_HEIGHT / 2; c < ARENA_HEIGHT / 2; ++c)
            {
                var bgBpx = new Rectangle(r * backgroundImg.Width + Tools.Screen.Center.X, c * backgroundImg.Height + Tools.Screen.Center.Y, backgroundImg.Width, backgroundImg.Height);
                spriteBatch.Draw(backgroundImg, bgBpx, null, Color.White, 0, new Vector2(backgroundImg.Width / 2F, backgroundImg.Height / 2F), SpriteEffects.None, 1f);
            }


            foreach (var mine in landmines)
            {
                mine.Draw(spriteBatch);
            }

            foreach (var item in itemsOnMap)
            {
                item.Draw(spriteBatch);
            }

            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }

            foreach (var bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }


            if (freeze)
            {
                spriteBatch.Draw(frozenScreen, Tools.ArenaBounds, Color.DarkRed * 0.3f);
            }

            player.Draw(spriteBatch);


            spriteBatch.Draw(enemyBaseImg, new Vector2(Tools.ArenaBounds.Left - enemyBaseImg.Width + 30, Tools.ArenaBounds.Bottom - enemyBaseImg.Height + 1), Color.White);
            spriteBatch.Draw(enemyBaseImg, new Vector2(Tools.ArenaBounds.Left - enemyBaseImg.Width + 30, -55), Color.White);

            spriteBatch.Draw(barrelImg, barrelBox, null, Color.White, MathHelper.ToRadians(45), new Vector2(barrelImg.Width * 0.5f, barrelImg.Height * 0.5f), SpriteEffects.None, 1);
            spriteBatch.Draw(crossHairs, Tools.TrueMousePos, null, Color.White, 0, new Vector2(crossHairs.Width * 0.5f, crossHairs.Height * 0.5f), 0.25f, SpriteEffects.None, 10);

            spriteBatch.End();

            spriteBatch.Begin();
            pauseBtn.Draw(spriteBatch);
            spriteBatch.End();


            inventory.Draw(spriteBatch);
        }

        /// <summary>
        /// Loads the path.
        /// </summary>
        private static void LoadPath()
        {
            var filePath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            filePath = Path.GetDirectoryName(filePath);

            // ReSharper disable PossibleNullReferenceException
            filePath = filePath.Substring(6);
            string foundPath = null;
            var found = false;
            while (filePath.Length > 3 && !(found = File.Exists(foundPath = filePath + "/saves/MyFile.txt")))
            {
                filePath = Path.GetDirectoryName(filePath);
            }
            // ReSharper restore PossibleNullReferenceException

            if (!found)
            {
                return;
            }

            filePath = foundPath;

            var curPath = 0;
            var points = new List<Vector2>();
            using (var inFile = File.OpenText(filePath))
            {
                while (!inFile.EndOfStream)
                {
                    string line;
                    while (!inFile.EndOfStream && (line = inFile.ReadLine()) != "#")
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        var data = line.Split(',');
                        points.Add(new Vector2((float) Convert.ToDouble(data[0]), (float) Convert.ToDouble(data[1])));
                    }

                    pathPoints[curPath] = points;
                    points = new List<Vector2>();
                    ++curPath;
                }
            }
        }

        /// <summary>
        /// Adds the bullet.
        /// </summary>
        /// <param name="bullet">The bullet.</param>
        public static void AddBullet(Bullet bullet)
        {
            if (freeze)
            {
                return;
            }

            bullets.Add(bullet);
        }

        /// <summary>
        /// Updates the mines.
        /// </summary>
        private static void UpdateMines()
        {
            //Create mines
            if (landMineTimer.IsTimeUp(Tools.GameTime) && Keyboard.GetState().IsKeyDown(Keys.E))
            {
                landMineTimer.Reset();
                landmines.Add(new BlueMine(player.GetPos()));
            }


            //Update mines
            for (var i = 0; i < landmines.Count; ++i)
            {
                if (landmines[i].Update())
                {
                    landmines.RemoveAt(i);
                    --i;
                }
            }

            //Check collision
            foreach (var landmine in landmines)
            {
                //check on player
                if (landmine is RedMine)
                {
                    if (Tools.BoxBoxCollision(player.GetRotatedRectangle(), landmine.GetBox()))
                    {
                        landmine.Collide();
                    }

                    continue;
                }

                //check on enemy;
                foreach (var enemy in enemies)
                {
                    if (Tools.BoxBoxCollision(enemy.GetRotatedRectangle(), landmine.GetBox()))
                    {
                        landmine.Collide();
                    }

                    if (landmine.IsActive() && Tools.CirclePointCollision(landmine.GetExplosionArea(), enemy.GetPos()))
                    {
                        enemy.Collide(landmine);
                    }
                }
            }
        }

        /// <summary>
        /// Updates the enemies.
        /// </summary>
        private static void UpdateEnemies()
        {
            //Spawn Enemies
            if (enemyTimer.IsTimeUp(Tools.GameTime) && enemies.Count < 20)
            {
                enemyTimer.Reset();
                switch (Tools.Rnd.Next(0, 100))
                {
                    case int n when n < 33:
                        enemies.Add(new TierOneEnemy(pathPoints[0][0], pathPoints[0], Stage.Low, 0));
                        break;
                    case int n when n < 66:
                        bomberEnemyTimer.Reset();
                        var angle = MathHelper.ToRadians(Tools.Rnd.Next(0, 361));
                        var pos = new Vector2((float) Math.Cos(angle), (float) Math.Sin(angle)) * ENEMY_SPAWN_DIS;
                        pos = player.GetPos() + pos;

                        enemies.Add(new BomberEnemy(pos, 0, Stage.Low));
                        break;
                    case int n when n < 100:
                        enemies.Add(new TierFourEnemy(new Vector2(Tools.ArenaBounds.Center.X, Tools.ArenaBounds.Y - 50), 0, Stage.Low, pathPoints[1]));
                        break;
                }
            }

            //Update Enemies
            for (var i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].Update(player.GetPos()))
                {
                    enemies.RemoveAt(i);
                    --i;
                }
            }

            //Enemy to player collision
            foreach (var enemy in enemies)
            {
                if (enemy.GetHealth() <= 0)
                {
                    continue;
                }

                if (Tools.BoxBoxCollision(enemy.GetRotatedRectangle(), player.GetRotatedRectangle()))
                {
                    enemy.Collide(player);
                    player.Collide(enemy);
                }
            }

            //Enemy to enemy collision
            for (var i = 0; i < enemies.Count; i++)
            {
                var enemy1 = enemies[i];
                if (enemy1.GetHealth() <= 0)
                {
                    continue;
                }

                for (var k = 0; k < enemies.Count; k++)
                {
                    var enemy2 = enemies[k];
                    if (enemy1.Equals(enemy2) || enemy2.GetHealth() <= 0)
                    {
                        continue;
                    }

                    if (enemy2 is HealerEnemy healer && Tools.CirclePointCollision(healer.HealArea(), enemy1.GetPos()))
                    {
                        enemy1.Heal(healer.HealAmount);
                    }

                    if (Tools.BoxBoxCollision(enemy1.GetRotatedRectangle(), enemy2.GetRotatedRectangle()))
                    {
                        enemies.RemoveAt(i);
                        enemies.RemoveAt(k - 1);
                        --i;

                        if (enemy1.GetTankType() == enemy2.GetTankType())
                        {
                            enemies.Add(MergeTanks(enemy1, enemy2));
                        }
                        else
                        {
                            var newPos = (enemy1.GetPos() + enemy2.GetPos()) / 2;
                            var newEnemy = combos[(int) enemy1.GetTankType(), (int) enemy2.GetTankType()];
                            enemies.Add(newEnemy == null 
                                ? MergeTanks(enemy1, enemy2) 
                                : newEnemy.Clone(newPos, enemy2.GetRotation(), Stage.Low));
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the bullets.
        /// </summary>
        private static void UpdateBullets()
        {
            //Update bullets
            for (var i = 0; i < bullets.Count; ++i)
            {
                if (bullets[i].Update())
                {
                    bullets.RemoveAt(i);
                    --i;
                }
            }

            //Bullet to player collision
            foreach (var bullet in bullets)
            {
                if (bullet.IsDead)
                {
                    continue;
                }

                if (bullet.BulletOwner == Owner.Enemy && Tools.BoxBoxCollision(player.GetRotatedRectangle(), bullet.GetRotatedRectangle()))
                {
                    player.Collide(bullet);
                    bullet.Collide();
                }
            }

            //Bullet enemy collision
            foreach (var enemy in enemies)
            foreach (var bullet in bullets)
            {
                if (bullet.IsDead)
                {
                    continue;
                }

                if (bullet.BulletOwner == Owner.Player && Tools.BoxBoxCollision(enemy.GetRotatedRectangle(), bullet.GetRotatedRectangle()))
                {
                    enemy.Collide(bullet);
                    bullet.Collide();
                }
            }
        }

        /// <summary>
        /// Updates the items.
        /// </summary>
        private static void UpdateItems()
        {
            for (var i = 0; i < itemsOnMap.Count; i++)
            {
                var item = itemsOnMap[i];
                if (Tools.BoxBoxCollision(item.Box, player.GetRotatedRectangle()))
                {
                    itemsOnMap.RemoveAt(i);
                    --i;
                    inventory.AddItem(item);
                }
            }
        }

        /// <summary>
        /// Adds the land mine.
        /// </summary>
        /// <param name="mine">The mine.</param>
        public static void AddLandMine(LandMine mine)
        {
            landmines.Add(mine);
        }

        /// <summary>
        /// Merges the tanks.
        /// </summary>
        /// <param name="tank1">The tank1.</param>
        /// <param name="tank2">The tank2.</param>
        /// <returns>Tank.</returns>
        private static Tank MergeTanks(Tank tank1, Tank tank2)
        {
            var newPos = (tank1.GetPos() + tank2.GetPos()) / 2;
            var stage = (Stage) (Math.Max((int) tank1.GetStage(), (int) tank2.GetStage()) + 1);
            stage = (Stage) Math.Min((int) stage, (int) (Stage.Player - 1));

            return tank1.Clone(newPos, tank2.GetRotation(), stage);
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public static void AddItem(Item item)
        {
            itemsOnMap.Add(item);
        }

        /// <summary>
        /// Freezes the game.
        /// </summary>
        public static void FreezeGame()
        {
            freezeTimer.Reset();
        }

        /// <summary>
        /// Makes the player leave shop.
        /// </summary>
        public static void MakePlayerLeaveShop()
        {
            player.StepOutOfShop();
        }

        /// <summary>
        /// Gets the current credit.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public static int GetCurrentCredit() => inventory.GetCurrentCredit();

        /// <summary>
        /// Gives the player new cannon.
        /// </summary>
        /// <param name="cannon">The cannon.</param>
        public static void GivePlayerNewCannon(Cannon cannon)
        {
            player.TakeCannon(cannon);
            inventory.Pay();
        }

        /// <summary>
        /// Speeds up player.
        /// </summary>
        public static void SpeedUpPlayer()
        {
            player.SpeedUp();
        }

        /// <summary>
        /// Ares any bullets left.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreAnyBulletsLeft() => inventory.AreAnyBulletsLeft();

        /// <summary>
        /// Removes the bullet.
        /// </summary>
        public static void RemoveBullet()
        {
            inventory.RemoveBullet();
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public static void Reset()
        {
            enemies.Clear();
            bullets.Clear();
            itemsOnMap.Clear();
            player = new Player(Tools.Screen.Center.ToVector2());
            inventory = new Inventory();
        }

        /// <summary>
        /// Games the is frozen.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool GameIsFrozen() => freeze;
    }
}