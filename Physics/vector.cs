namespace Physics
{
    public readonly struct Vector
    {
        public readonly float X;
        public readonly float Y;
        public static readonly Vector Zero = new Vector(0f, 0f);

        public Vector(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector operator +(Vector a, Vector b) => new Vector(a.X + b.X, a.Y + b.Y);
        public static Vector operator -(Vector a, Vector b) => new Vector(a.X - b.X, a.Y - b.Y);
        public static Vector operator -(Vector v) => new Vector(-v.X, -v.Y);
        public static Vector operator *(Vector v, float s) => new Vector(v.X * s, v.Y * s);
        public static Vector operator /(Vector v, float s) => new Vector(v.X / s, v.Y / s);

        public bool Equal(Vector other) => this.X == other.X && this.Y == other.Y;
        public override bool Equals(object obj)
        {
            if (obj is Vector other)
            {
                return this.Equal(other);
            }

            return false;
        }

        public override int GetHashCode() => new { this.X, this.Y }.GetHashCode();
        public override string ToString() => $"({this.X}, {this.Y})";
    }
}