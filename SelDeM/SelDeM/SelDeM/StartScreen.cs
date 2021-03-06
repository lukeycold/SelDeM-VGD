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
    class StartScreen //Logo and controls
    {
        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D controlTexture, logoTexture;
        Rectangle controlPosition, logoPosition;
        String controlText;
        Vector2 textPosition;
        GraphicsDeviceManager graphics;
        bool isShowing;

        public StartScreen(SpriteBatch spriteBatch, ContentManager contentManager, GraphicsDeviceManager graphics)
        {
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;

            controlTexture = contentManager.Load<Texture2D>("Seldem controls");
            logoTexture = contentManager.Load<Texture2D>("Seldem Logo");

            controlPosition = new Rectangle(graphics.PreferredBackBufferWidth/2 - (600/2), graphics.PreferredBackBufferHeight/2, 600, 200);
            logoPosition = new Rectangle(graphics.PreferredBackBufferWidth/2 - (816/2), 0, 816, 215);

            controlText = "WASD - Movement\nSPACE/ENTER - Interact and Confirm\nESCAPE - Close Game\nARROW KEYS(RIGHT/LEFT) - Progress Dialog\nARROW KEYS(UP/DOWN) - Highlight Choice\nTHIS IS A DEMO";

            textPosition = new Vector2(controlPosition.Left, controlPosition.Bottom);

            font = contentManager.Load<SpriteFont>("StartScreenFont");
            isShowing = true;
        }

        public void Update(GameTime gameTime, KeyboardState kb, KeyboardState oldkb)
        {
            if (isShowing)
            {
                if (kb.IsKeyDown(Keys.Enter)&& !oldkb.IsKeyDown(Keys.Enter))
                {
                    isShowing = false;
                }
            }
        }

        public bool Showing
        {
            get { return isShowing; }
            set { isShowing = value; }
        }

        public void Draw()
        {
            spriteBatch.Draw(controlTexture, controlPosition, Color.White);
            spriteBatch.Draw(logoTexture, logoPosition, Color.White);
            spriteBatch.DrawString(font, controlText, textPosition, Color.Black);
        }
    }
}
