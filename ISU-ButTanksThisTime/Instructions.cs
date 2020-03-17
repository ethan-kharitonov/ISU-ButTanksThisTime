// Author        : Ethan Kharitonov
// File Name     : Instructions.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : The instruction scene.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    static class Instructions
    {
        //store the backround image of the scene
        private static Texture2D bgImg;

        //store the instructions image
        private static Texture2D instructionsImg;

        //store the back to menu button
        private static Button backBtn;

        static Instructions()
        {
            //load background and isntruction images
            bgImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ShopBackground");
            instructionsImg = Tools.Content.Load<Texture2D>("Images/Backgrounds/ethan isu 2");

            //load back to main menu button
            Texture2D backBtnImg = Tools.Content.Load<Texture2D>("Images/Sprites/UI/BTN BACK");
            backBtn = new Button(backBtnImg, new Rectangle(Tools.Screen.Center.X - 50, 450, 100, 50), "");
        }

        /// <summary>
        /// updates the instructions scene
        /// </summary>
        public static void Update()
        {
            //check if back button is pressed
            if (backBtn.Update())
            {
                //go back to menu
                TankGame.State = GameState.Menu;
            }
        }

        /// <summary>
        /// draws the instructions scene
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //draw the bacgkround and instructions images
            spriteBatch.Draw(bgImg, Tools.Screen, Color.White);
            spriteBatch.Draw(instructionsImg, Tools.Screen, Color.White);

            //Draw the back button
            backBtn.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
