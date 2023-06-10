using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Dihydrogen_Monoxide
{
    internal class AddBall : IBlock
    {
        Vector2 possition;
        public Vector2 Possition { get => possition; set { possition = value; } }
        int hp;
        public int Hp { get => hp; set { hp = value; } }
        int size;
        public int Size { get => size; set { size = value; } }

        public AddBall() { possition = Vector2.Zero; hp = 0; size = 0; }
        public AddBall(Vector2 vec, int hap, int siz) { possition = vec; hp = hap; size = siz; }

        public int Colision(Vector2 bal, int bal_rad)
        {

            return 0;
        }

        public void Print()
        {

        }

    }
}
