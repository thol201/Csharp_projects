using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Dihydrogen_Monoxide
{
    internal class Ball
    {
        Vector2 possition;
        public Vector2 Possition { get => possition; set { possition = value; } }
        Vector2 velocity;
        public Vector2 Velocity { get => velocity; set { velocity = value; } }
        Color color;
        public Color Color { get => color;set { color = value; } }
        
        public Ball() { possition = Vector2.Zero;velocity = Vector2.Zero;color = Color.White; }
        public Ball(Vector2 pos,Vector2 vel,Color col) { possition = pos;velocity = vel;color = col; }

    }
}
