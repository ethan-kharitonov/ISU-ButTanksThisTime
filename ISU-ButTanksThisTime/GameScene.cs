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

        //stores the player
        private static Player player;

        //Bullet Variables
        private static readonly List<Bullet> bullets = new List<Bullet>();
        private static readonly List<LandMine> landmines = new List<LandMine>();
        private static readonly Timer landMineTimer = new Timer(2000);

        //Basic Enemy Variables
        private static readonly Timer enemyTimer = new Timer(2000);
        private const int ENEMY_SPAWN_DIS = 1000;
        private const int MAX_ENEMIES = 35;

        //Bomber Enemy Variables
        private static readonly Timer bomberEnemyTimer = new Timer(4000);

        //Barrel Variables
        private static Texture2D barrelImg;
        private static Rectangle shopBox;


        //Load Path Variables
        private static readonly List<Vector2>[] pathPoints = new List<Vector2>[3];


        //Enemy Variables
        private static readonly List<Tank> enemies = new List<Tank>();
        private static Texture2D enemyBaseImg;


        //Camera Variables
        private static Camera camera;

        //store the different tank combinations
        private static readonly Tank[,] combos = new Tank[6, 6];

        //store the image of the crosshairs
        private static Texture2D crossHairs;

        //store the relocate power variables
        private static readonly Timer freezeTimer = new Timer(4000);
        private static bool freeze;
        private static Texture2D frozenScreen;


        //Inventory variables
        private static readonly List<Item> itemsOnMap = new List<Item>();
        private static Inventory inventory = new Inventory();

        //store th pause button
        private static Button pauseBtn;

        /// <summary>
        /// Loads the content.
        /// </summary>
        public static void LoadContent()
        {
            //create the camera
            camera = new Camera(Tools.Graphics.Viewport);

            //Load All Images//
            backgroundImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/bg");
            Tools.Content.Load<Texture2D>("Images/Sprites/Bullets/Medium_Shell");
            barrelImg = Tools.Content.Load<Texture2D>("Images/Sprites/Terrain/Container_B");
            crossHairs = Tools.Content.Load<Texture2D>("Images/Sprites/Crosshairs/crosshair068");
            enemyBaseImg = Tools.Content.Load<Texture2D>("Images/Sprites/Terrain/Container_D");
            ///////////////////

            //create the pause button
            pauseBtn = new Button(Tools.ButtonImg, new Rectangle(15, 15, 100, 50), "PAUSE");

            //load the map rectangle into Tools.ArenaBounds
            var arenaXPos = -(ARENA_WIDTH / 2) * backgroundImg.Width + Tools.Screen.Center.X - backgroundImg.Width / 2;
            var arenaYPos = -ARENA_HEIGHT / 2 * backgroundImg.Height + Tools.Screen.Center.Y - backgroundImg.Height / 2;
            var arenaWidth = ARENA_WIDTH * backgroundImg.Width;
            var arenaHeight = ARENA_HEIGHT * backgroundImg.Height;
            Tools.ArenaBounds = new Rectangle(arenaXPos, arenaYPos, arenaWidth, arenaHeight);

            //create the player
            player = new Player(Tools.Screen.Center.ToVector2());

            //load the enmey paths
            LoadPath();

            //load the shops rectangle
            shopBox = new Rectangle(Tools.ArenaBounds.Left, Tools.ArenaBounds.Top, barrelImg.Width, barrelImg.Height * 2);

            //set all combos originally to null
            for (var i = 0; i < 5; ++i)
            for (var k = 0; k < 5; ++k)
            {
                combos[i, k] = null;
            }

            //create all the combinations
            combos[(int) TankType.BasicPath, (int) TankType.Bomber] = new HealerEnemy(Vector2.Zero, 0, Stage.Low, pathPoints[2]);
            combos[(int) TankType.BasicPath, (int) TankType.RotateShooter] = new BurstEnemy(Vector2.Zero, 0, Stage.Low);

            combos[(int) TankType.Bomber, (int) TankType.BasicPath] = new HealerEnemy(Vector2.Zero, 0, Stage.Low, pathPoints[2]);
            combos[(int) TankType.Bomber, (int) TankType.RotateShooter] = new TierTwoEnemy(Vector2.Zero, 0, Stage.Low);

            combos[(int) TankType.RotateShooter, (int) TankType.BasicPath] = new BurstEnemy(Vector2.Zero, 0, Stage.Low);
            combos[(int) TankType.RotateShooter, (int) TankType.Bomber] = new TierTwoEnemy(Vector2.Zero, 0, Stage.Low);

            //create the frozen screen img
            frozenScreen = new Texture2D(Tools.Graphics, Tools.ArenaBounds.Width, Tools.ArenaBounds.Height);
            var data = new Color[Tools.ArenaBounds.Width * Tools.ArenaBounds.Height];
            for (var i = 0; i < data.Length; ++i)
            {
                data[i] = Color.LightGray;
            }
            frozenScreen.SetData(data);
        }

        /// <summary>
        /// Updates the main part of the game
        /// </summary>
        public static void Update()
        {
            //update the camera
            camera.Update(player.GetPos());

            //update the Tools true mouse position
            var screenTl = player.GetPos() - Tools.Screen.Size.ToVector2() / 2;
            screenTl.X = MathHelper.Clamp(screenTl.X, Tools.ArenaBounds.Left, Tools.ArenaBounds.Right - Tools.Screen.Width / 2f);
            screenTl.Y = MathHelper.Clamp(screenTl.Y, Tools.ArenaBounds.Top, Tools.ArenaBounds.Bottom - Tools.Screen.Height / 2);
            Tools.TrueMousePos = Mouse.GetState().Position.ToVector2() + screenTl;

            //check if pause button is clicked
            if (pauseBtn.Update())
            {
                //go to pause state
                TankGame.State = GameState.Pause;
            }
            
            //update inventory
            inventory.Update();

            //update player
            player.Update(Vector2.Zero);

            //check if player is in shop
            var trueBarrelBox = shopBox;
            trueBarrelBox.Height = (int) (trueBarrelBox.Height * 0.60);
            if (Tools.BoxBoxCollision(player.GetRotatedRectangle(), new RotatedRectangle(trueBarrelBox, 45, new Vector2(shopBox.Width / 2f, shopBox.Height / 2f))))
            {
                //go to shop state
                TankGame.State = GameState.Shop;
            }

            //check if game is frozen
            freeze = !freezeTimer.IsTimeUp(Tools.GameTime);
            if (freeze)
            {
                //leave update
                return;
            }

            //update mines, enemies, bullets and items
            UpdateMines();
            UpdateEnemies();
            UpdateBullets();
            UpdateItems();
        }

        /// <summary>
        /// draws the main part of the game
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            //starts a spriteBatch with the camera details
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            //draws the background
            for (var r = -ARENA_WIDTH / 2; r < ARENA_WIDTH / 2; ++r)
            for (var c = -ARENA_HEIGHT / 2; c < ARENA_HEIGHT / 2; ++c)
            {
                var bgBpx = new Rectangle(r * backgroundImg.Width + Tools.Screen.Center.X, c * backgroundImg.Height + Tools.Screen.Center.Y, backgroundImg.Width, backgroundImg.Height);
                spriteBatch.Draw(backgroundImg, bgBpx, null, Color.White, 0, new Vector2(backgroundImg.Width / 2F, backgroundImg.Height / 2F), SpriteEffects.None, 1f);
            }

            //draws the landmines
            foreach (var mine in landmines)
            {
                //draw all mines
                mine.Draw(spriteBatch);
            }

            //draws the items on the map
            foreach (var item in itemsOnMap)
            {
                //draw all items on map
                item.Draw(spriteBatch);
            }

            //draws all the enemies
            foreach (var enemy in enemies)
            {
                //draw all enemies
                enemy.Draw(spriteBatch);
            }

            //draws all the bullets
            foreach (var bullet in bullets)
            {
                //draw bullets
                bullet.Draw(spriteBatch);
            }

            //draw the freeze game image over all other objects
            if (freeze)
            {
                //draw the freeze game image
                spriteBatch.Draw(frozenScreen, Tools.ArenaBounds, Color.DarkRed * 0.3f);
            }

            //draw the player
            player.Draw(spriteBatch);

            //draw the enemie bases (what the enemies go out of and back into)
            spriteBatch.Draw(enemyBaseImg, new Vector2(Tools.ArenaBounds.Left - enemyBaseImg.Width + 30, Tools.ArenaBounds.Bottom - enemyBaseImg.Height + 1), Color.White);
            spriteBatch.Draw(enemyBaseImg, new Vector2(Tools.ArenaBounds.Left - enemyBaseImg.Width + 30, -55), Color.White);

            //draw the shop
            spriteBatch.Draw(barrelImg, shopBox, null, Color.White, MathHelper.ToRadians(45), new Vector2(barrelImg.Width * 0.5f, barrelImg.Height * 0.5f), SpriteEffects.None, 1);
            
            //draw the crosshairs
            spriteBatch.Draw(crossHairs, Tools.TrueMousePos, null, Color.White, 0, new Vector2(crossHairs.Width * 0.5f, crossHairs.Height * 0.5f), 0.25f, SpriteEffects.None, 10);

            spriteBatch.End();

            spriteBatch.Begin();
            //draw the pause button
            pauseBtn.Draw(spriteBatch);
            spriteBatch.End();

            //draw the inventory
            inventory.Draw(spriteBatch);
        }

        /// <summary>
        /// Load the enemie paths from a text file
        /// </summary>
        private static void LoadPath()
        {
            // Start from the directory where the executable is found
            var filePath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            filePath = Path.GetDirectoryName(filePath);

            // Search for the file saves/MyFile.txt
            // Start from the directory where the binary is.
            filePath = filePath.Substring(6);
            string foundPath = null;
            var found = false;
            while (filePath != null && !(found = File.Exists(foundPath = filePath + "/saves/MyFile.txt")))
            {
                // If not found go up one level and check whether saves/MyFile.txt is found there
                filePath = Path.GetDirectoryName(filePath);
            }

            if (!found)
            {
                // Did not find saves/MyFile.txt anywhere from the folder of the binary up to the root
                return;
            }

            // Found saves/MyFile.txt - prepare to read the path information from there.
            filePath = foundPath;

            //stores the index of the current path being loaded
            var curPath = 0;

            //will store the path as its being loadad
            var points = new List<Vector2>();

            using (var inFile = File.OpenText(filePath))
            {
                while (!inFile.EndOfStream)
                {
                    //will store one line on the text file
                    string line;
                    while (!inFile.EndOfStream && (line = inFile.ReadLine()) != "#")
                    {
                        //if there is no information on that line continue
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        //stores one point in the path
                        var data = line.Split(',');
                        
                        //add the current point onto points
                        points.Add(new Vector2((float) Convert.ToDouble(data[0]), (float) Convert.ToDouble(data[1])));
                    }

                    //load points int the current path and increament the current path index
                    pathPoints[curPath] = points;
                    ++curPath;
                    
                    //clear points so it could be re-used
                    points = new List<Vector2>();
                }
            }
        }

        /// <summary>
        /// Adds a given bullet bullet.
        /// </summary>
        /// <param name="bullet">The bullet which will be added.</param>
        public static void AddBullet(Bullet bullet)
        {
            //if game is frozen dont add
            if (freeze)
            {
                return;
            }

            //add the bullet
            bullets.Add(bullet);
        }

        /// <summary>
        /// Updates the mines.
        /// </summary>
        private static void UpdateMines()
        {
            //add landmine if time between mines is up and user pressed E
            if (landMineTimer.IsTimeUp(Tools.GameTime) && Keyboard.GetState().IsKeyDown(Keys.E))
            {
                //reset land mine timer
                landMineTimer.Reset();

                //add a landmine
                landmines.Add(new BlueMine(player.GetPos()));
            }


            //Update each min mines
            for (var i = 0; i < landmines.Count; ++i)
            {
                //if the mine returned true remove it
                if (landmines[i].Update())
                {
                    //remove the mine and decreament i to avoid skipping any
                    landmines.RemoveAt(i);
                    --i;
                }
            }

            //Check collision for each mine
            foreach (var landmine in landmines)
            {
                //check on player if mine is red
                if (landmine is RedMine)
                {
                    if (Tools.BoxBoxCollision(player.GetRotatedRectangle(), landmine.GetBox()))
                    {
                        //if collision exists trigger mine
                        landmine.Collide();
                    }

                    //check if player is hit by active mine
                    if (landmine.IsActive && Tools.CirclePointCollision(landmine.GetExplosionArea(), player.GetPos()))
                    {
                        //collide player with mine
                        player.Collide(landmine);
                    }

                    //red mines do not need to be checked against enemies
                    continue;
                }

                //check on enemy;
                foreach (var enemy in enemies)
                {
                    //check if player hit mine
                    if (Tools.BoxBoxCollision(enemy.GetRotatedRectangle(), landmine.GetBox()))
                    {
                        //trigger mine
                        landmine.Collide();
                    }

                    //check if enemies are hit by active mine
                    if (landmine.IsActive && Tools.CirclePointCollision(landmine.GetExplosionArea(), enemy.GetPos()))
                    {
                        //collide enemy with mine
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
            //Spawn Enemies on timer if max not reached
            if (enemyTimer.IsTimeUp(Tools.GameTime) && enemies.Count < MAX_ENEMIES)
            {
                //reset timer
                enemyTimer.Reset();

                //spawn a random enemie
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
                //remove enemie if they return true
                if (enemies[i].Update(player.GetPos()))
                {
                    //remove enemie
                    enemies.RemoveAt(i);
                    --i;
                }
            }

            //Enemy to player collision
            foreach (var enemy in enemies)
            {
                //skip enemy if it is dead
                if (enemy.GetHealth() <= 0)
                {
                    continue;
                }

                //check enemy to player collision
                if (Tools.BoxBoxCollision(enemy.GetRotatedRectangle(), player.GetRotatedRectangle()))
                {
                    //collide enemie and player to each other
                    enemy.Collide(player);
                    player.Collide(enemy);
                }
            }

            //Enemy to enemy collision
            for (var i = 0; i < enemies.Count; i++)
            {
                var enemy1 = enemies[i];
                //if enemy1 is dead skip
                if (enemy1.GetHealth() <= 0)
                {
                    continue;
                }

                for (var k = 0; k < enemies.Count; k++)
                {
                    var enemy2 = enemies[k];
                    
                    //if enemy2 is dead or enemy1 is enemy2 skip
                    if (enemy1.Equals(enemy2) || enemy2.GetHealth() <= 0)
                    {
                        continue;
                    }

                    //check if player is in healer enemy circle
                    if (enemy2 is HealerEnemy healer && Tools.CirclePointCollision(healer.HealArea(), enemy1.GetPos()))
                    {
                        //heal the enemy
                        enemy1.Heal(healer.HealAmount);
                    }

                    //chekc if the two enemies colliding
                    if (Tools.BoxBoxCollision(enemy1.GetRotatedRectangle(), enemy2.GetRotatedRectangle()))
                    {
                        //remove both enemies
                        enemies.RemoveAt(i);
                        enemies.RemoveAt(k - 1);
                        --i;

                        //if they are the same type upgrade their stage
                        if (enemy1.GetTankType() == enemy2.GetTankType())
                        {
                            //merge the two enemys
                            enemies.Add(MergeTanks(enemy1, enemy2));
                        }
                        else
                        {
                            //calculate the position and type of the new enemy
                            var newPos = (enemy1.GetPos() + enemy2.GetPos()) / 2;
                            var newEnemy = combos[(int) enemy1.GetTankType(), (int) enemy2.GetTankType()];
                            
                            //adds the new enemy to the list (if this combination is not defined then merge the two enemies instead instead)
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
                //remove the bullet if it returns true
                if (bullets[i].Update())
                {
                    //remove the bullet
                    bullets.RemoveAt(i);
                    --i;
                }
            }

            //Bullet to player collision
            foreach (var bullet in bullets)
            {
                //if the bullet is dead skip
                if (bullet.IsDead)
                {
                    continue;
                }

                //chekc if the bullet is an enemy bullet and collides with player
                if (bullet.BulletOwner == Owner.Enemy && Tools.BoxBoxCollision(player.GetRotatedRectangle(), bullet.GetRotatedRectangle()))
                {
                    //collide player and bullet to each other
                    player.Collide(bullet);
                    bullet.Collide();
                }
            }

            //Bullet enemy collision
            foreach (var enemy in enemies)
            foreach (var bullet in bullets)
            {
                //if the bullet is desd skip
                if (bullet.IsDead)
                {
                    continue;
                }

                //check if the bullet is a player bullet and collides with the enemy
                if (bullet.BulletOwner == Owner.Player && Tools.BoxBoxCollision(enemy.GetRotatedRectangle(), bullet.GetRotatedRectangle()))
                {
                    //collide the enemy and the bullet to each other
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
            //checks if any items on the map are colliding with the player
            for (var i = 0; i < itemsOnMap.Count; i++)
            {
                var item = itemsOnMap[i];
                //check if colliding player
                if (Tools.BoxBoxCollision(item.Box, player.GetRotatedRectangle()))
                {
                    //remove that otem
                    itemsOnMap.RemoveAt(i);
                    --i;

                    //add it to the players inventory
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
            //adds the given land mine
            landmines.Add(mine);
        }

        /// <summary>
        /// Merges the tanks into a third tank
        /// </summary>
        /// <param name="tank1">tank1.</param>
        /// <param name="tank2">tank2.</param>
        /// <returns>Tank.</returns>
        private static Tank MergeTanks(Tank tank1, Tank tank2)
        {
            //calculte the position and stage of the new tank
            var newPos = (tank1.GetPos() + tank2.GetPos()) / 2;
            var stage = (Stage) (Math.Max((int) tank1.GetStage(), (int) tank2.GetStage()) + 1);

            //makes sure the stage is not above the max
            stage = (Stage) Math.Min((int) stage, (int) (Stage.Player - 1));

            //returns the new tank
            return tank1.Clone(newPos, tank2.GetRotation(), stage);
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public static void AddItem(Item item)
        {
            //puts the given item
            itemsOnMap.Add(item);
        }

        /// <summary>
        /// Freezes the game.
        /// </summary>
        public static void FreezeGame()
        {
            //resets the time left in the freeze
            freezeTimer.Reset();
        }

        /// <summary>
        /// Makes the player leave shop.
        /// </summary>
        public static void MakePlayerLeaveShop()
        {
            //moves player out of the shop
            player.StepOutOfShop();
        }

        /// <summary>
        /// Gets the current amount of coins.
        /// </summary>
        /// <returns>the number of coins the player has</returns>
        public static int GetCurrentCredit() => inventory.GetCurrentCredit();

        /// <summary>
        /// Gives the player new cannon.
        /// </summary>
        /// <param name="cannon">The cannon that will be given to the player.</param>
        public static void GivePlayerNewCannon(Cannon cannon, int price)
        {
            //give player the cannon and make him pay for it
            player.TakeCannon(cannon);
            inventory.Pay(price);
        }

        /// <summary>
        /// Speeds up player.
        /// </summary>
        public static void SpeedUpPlayer()
        {
            //make player speed up
            player.SpeedUp();
        }

        /// <summary>
        /// checks if there is any ammo left.
        /// </summary>
        /// <returns><c>true</c> if ammo above 0, <c>false</c> otherwise.</returns>
        public static bool AreAnyBulletsLeft() => inventory.AreAnyBulletsLeft();

        /// <summary>
        /// Removes a bullet.
        /// </summary>
        public static void RemoveBullet()
        {
            //removes a bullet
            inventory.RemoveBullet();
        }

        /// <summary>
        /// Resets the game.
        /// </summary>
        public static void Reset()
        {
            //reset all variables that changes during the game play
            enemies.Clear();
            bullets.Clear();
            itemsOnMap.Clear();
            player = new Player(Tools.Screen.Center.ToVector2());
            inventory = new Inventory();
        }

        /// <summary>
        /// returns wether the game is frozen
        /// </summary>
        /// <returns><c>true</c> if the game is frozen, <c>false</c> otherwise.</returns>
        public static bool GameIsFrozen() => freeze;
    }
}