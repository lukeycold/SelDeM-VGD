﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SelDeM
{
    class Tile
    {
        Rectangle rect;
        enum TileFlags { plain, unwalkable }
        TileFlags flag;
        int ForcefieldOffset = 0;

        public Tile(Rectangle r, String tileType)
        {
            rect = r;
            setTile(tileType);
        }
        public Tile(Rectangle r)
        {
            rect = r;
            flag = TileFlags.plain;
        }

        public int TileForcefield
        {
            get { return ForcefieldOffset; }
            set { ForcefieldOffset = value; }
        }

        public Rectangle Rectangle
        {
            get
            {
                return this.rect;
            }
            set
            {
                this.rect = value;
            }
        }

        public Boolean checkFlagForPlayer(Player player)
        {
            switch (flag)
            {
                case TileFlags.plain:
                    {
                        return rect.Intersects(player.Rectangle);
                    }
                case TileFlags.unwalkable:
                    {
                        if (rect.Intersects(player.Rectangle))
                        {
                            Vector2 direction = new Vector2(0,0);
                            //Moves inserted rectangle away from tile depending on closest side (This should not happen as it will make the player jitter if attempted to apply opposite force)
                            if (player.Rectangle.X + player.Rectangle.Width + player.Speed <= rect.X + rect.Width / 2 && player.PlayerKeyboard.IsKeyDown(Keys.D))
                            {
                                player.CanWalk = false;
                            }
                            else if(player.Rectangle.X - player.Speed> rect.X + rect.Width / 2 && player.PlayerKeyboard.IsKeyDown(Keys.A))
                            {
                                player.CanWalk = false;
                            }
                            else if(player.Rectangle.Y - player.Speed <= rect.Y + rect.Height / 2 && player.PlayerKeyboard.IsKeyDown(Keys.S))
                            {
                                player.CanWalk = false;
                            }
                            else if(player.Rectangle.Y + player.Rectangle.Height + player.Speed > rect.Y + rect.Height / 2 && player.PlayerKeyboard.IsKeyDown(Keys.W))
                            {
                                player.CanWalk = false;
                            }
                            player.move(direction, ForcefieldOffset);
                            return true;
                        }
                        return false;
                    }
            }
            return rect.Intersects(player.Rectangle);
        }

        public void setTile(String tileType)
        {
            switch (tileType.ToLower())
            {
                case "plain":
                    {
                        flag = TileFlags.plain;
                        break;
                    }
                case "unwalkable":
                    {
                        flag = TileFlags.unwalkable;
                        break;
                    }
                default:
                    {
                        flag = TileFlags.plain;
                        break;
                    }
            }
        }
    }
}
