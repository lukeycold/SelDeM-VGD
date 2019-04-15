﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace SelDeM
{
    class DialogueChoices
    {
        String[] confirm;
        bool choice, enterPressed;
        StreamReader reader;
        SpriteFont font;
        KeyboardState oldKB;
        Rectangle[] arrowRect;
        Texture2D arrowTexture;
        SpriteBatch spriteBatch;
        Vector2 vector0, vector1;

        public DialogueChoices(SpriteBatch spriteBatch, ContentManager contentManager, List<String> choices) //show these choices from list, and then return the int of choice chosen
        {
            oldKB = Keyboard.GetState();

            confirm = new String[2];
            choice = false; //Choice1 is true, Choice2 is false
            enterPressed = false;

            readChoices(choices);


            this.spriteBatch = spriteBatch;
            arrowTexture = contentManager.Load<Texture2D>("ChoiceArrow");

            //these vectors will change the location of both the text and the arrow image
            vector0 = new Vector2(50, 400); 
            vector1 = new Vector2(50, 420);
            arrowRect = new Rectangle[3];
            arrowRect[0] = new Rectangle(0, 0, 20, 32);
            arrowRect[1] = new Rectangle(0, 32, 20, 32);
            arrowRect[2] = new Rectangle((int)vector1.X- arrowRect[0].Width, (int)vector1.Y, arrowRect[0].Width, arrowRect[0].Height);
            font = contentManager.Load<SpriteFont>("DialogChoiceFont");
        }

        private void readChoices(List<String> choices)
        {
            confirm[0] = choices[0].ToString();
            confirm[1] = choices[1].ToString();
        }

        public void Input()
        {
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Up) && !oldKB.IsKeyDown(Keys.Up) && !enterPressed)
            {
                choice = true;
                arrowRect[2].Y = (int)vector0.Y;
            }

            if (kb.IsKeyDown(Keys.Down) && !oldKB.IsKeyDown(Keys.Down) && !enterPressed)
            {
                choice = false;
                arrowRect[2].Y = (int)vector1.Y;
            }
            if (kb.IsKeyDown(Keys.Enter) && !oldKB.IsKeyDown(Keys.Enter))
            {
                enterPressed = true;
            }
            kb = oldKB;
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (enterPressed != true)
            {
                //Arrow blinking
                if (gameTime.TotalGameTime.Seconds % 2 == 0)
                    spriteBatch.Draw(arrowTexture, arrowRect[2], arrowRect[0], Color.White, 0f, new Vector2(0,0), SpriteEffects.None, 0.99f);
                else
                    spriteBatch.Draw(arrowTexture, arrowRect[2], arrowRect[1], Color.White, 0f, new Vector2(0,0), SpriteEffects.None, 0.99f);
                //Choice text
                spriteBatch.DrawString(font, confirm[0], vector0, Color.White, 0f, new Vector2(0,0), 1f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(font, confirm[1], vector0, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
            }
            spriteBatch.End();
        }

        public int choiceChosen()
        {
            if(enterPressed)
            {
                if (choice == true)
                    return 0;
                else
                    return 1;
            }
            else
            {
                return -1;
            }
        }

        //is the end of branch, choice, the return integer

    }
}
