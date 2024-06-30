using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Physics;

namespace Sim;

public static class Converter
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ToVector2(Vector v) => new Vector2(v.X, v.Y);
}
