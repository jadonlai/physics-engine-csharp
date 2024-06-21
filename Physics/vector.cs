using System;

namespace Physics
{
    public readonly struct Vector
    {
        public readonly float X;
        public readonly float Y;

        public Vector(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }
    }
}