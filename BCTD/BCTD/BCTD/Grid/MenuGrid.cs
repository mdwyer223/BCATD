using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BCTD
{
    public class MenuGrid : Grid
    {
        private Random rand;
        private List<Tower> towers;
        private List<Enemy> enemies;

        public MenuGrid()
            : base()
        {
            rand = new Random();
            towers = new List<Tower>();
            enemies = new List<Enemy>();

            for (int i = 0; i < 2; i++)
            {
                int randRow = rand.Next(rows);
                int randCol = rand.Next(columns);

                towers.Add(new Tower(new Location(randRow, randCol), this));

                randRow = rand.Next(rows);
                randCol = rand.Next(columns);
                towers.Add(new HomingTower(new Location(randRow, randCol), this));
            }

            enemies.Add(new Enemy(this, new Entrance(new Location(6, 0), this)));
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Tower t in towers)
            {
                if(t!=null)
                {
                    if(enemies.Count != 0)
                    {
                        t.Update(enemies[0]);
                        enemies[0].Update(new Vector2(.2f, 0));
                        if (enemies[0].IsDead || enemies[0].Position.X > 800)
                        {
                            enemies[0].reset(new Location(6,0));
                        }
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            enemies[0].Draw(spriteBatch);
            foreach (Tower t in towers)
            {
                t.Draw(spriteBatch);
            }
        }
        
    }
}
