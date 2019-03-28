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

namespace SelDeM
{
    class DialogBox
    {
        private Rectangle dialogBoxRect;
        SpriteBatch spriteBatch;
        private SpriteFont sp1;
        private Texture2D dialogBoxTexture;
        ContentManager Content;
        GraphicsDeviceManager graphics;
        KeyboardState kb, oldkb;
        private string text;

        public DialogBox(SpriteBatch spriteBatch, ContentManager Content, GraphicsDeviceManager graphics, string text)
        {
            this.spriteBatch = spriteBatch;
            this.Content = Content;
            Content.RootDirectory = "Content";
            sp1 = Content.Load<SpriteFont>("DialogBoxFont");
            dialogBoxTexture = Content.Load<Texture2D>("txtbox");
            //creates dialog box that will take up the lower 1/5 of the screen
            this.graphics = graphics;
            int width = graphics.PreferredBackBufferWidth-100;
            int height = graphics.PreferredBackBufferHeight/5;
            dialogBoxRect = new Rectangle(50, graphics.PreferredBackBufferHeight-height-25, width, height);
            kb = Keyboard.GetState();
            oldkb = kb;
            this.text = feedText(text);
        }

        public string feedText(string t)
        {
            //wrap around text based on width of dialogbox
            string line = "";
            string formattedText = "";
            string[] words = t.Split(' ');
            foreach (string word in words)
            {
                if (sp1.MeasureString(line+word).Length()>dialogBoxRect.Width-(int)(dialogBoxRect.Width*.06))
                {
                    formattedText = formattedText + line + '\n';
                    line = "";
                }
                line = line + word + ' ';
            }
            return formattedText + line;
        }

        public Rectangle Rectangle
        {
            get { return dialogBoxRect; }
            set { dialogBoxRect = value; }
        }

        public Texture2D Texture
        {
            get { return dialogBoxTexture; }
            set { dialogBoxTexture = value; }
        }

        public SpriteFont SpriteFont
        {
            get { return sp1; }
            set { sp1 = value; }
        }

        public void update()
        {
            kb = Keyboard.GetState();
            oldkb = kb;
        }

        public void Draw()
        {
            spriteBatch.Draw(dialogBoxTexture, dialogBoxRect, Color.White);
            spriteBatch.DrawString(sp1, text, new Vector2(dialogBoxRect.X+(int)(dialogBoxRect.Width*.04), dialogBoxRect.Y+(int)(dialogBoxRect.Height*.1)), Color.White);
        }
    }
}
