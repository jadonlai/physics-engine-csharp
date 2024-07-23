using System;

namespace Physics;

public class PhysicsMath
{
    public static float Clamp(float value, float min, float max)
    {
        if (min == max) {
            return min;
        }

        if (min > max) {
            throw new ArgumentOutOfRangeException("min is greater than the max");
        }

        if (value < min) {
            return min;
        }

        if (value > max) {
            return max;
        }

        return value;
    }

    public static float Length(Vector v) => MathF.Sqrt(v.X * v.X + v.Y * v.Y);

    public static float Distance(Vector a, Vector b)
    {
        float dx = a.X - b.X;
        float dy = a.Y - b.Y;
        return MathF.Sqrt(dx * dx + dy * dy);
    }

    public static Vector Normalize(Vector v)
    {
        float len = Length(v);
        return new Vector(v.X / len, v.Y / len);
    }

    public static float Dot(Vector a, Vector b) => a.X * b.X + a.Y * b.Y;
    public static float Cross(Vector a, Vector b) => a.X * b.Y - a.Y * b.X;
}
