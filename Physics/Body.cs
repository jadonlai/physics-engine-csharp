using System;
using System.Formats.Asn1;
using System.Linq.Expressions;

namespace Physics;

public enum ShapeType
{
    Circle = 0,
    Box = 1
}

public sealed class Body
{
    private Vector position;
    private Vector linearVelocity;
    private float rotation;
    private float rotationalVelocity;

    public readonly float Density;
    public readonly float Mass;
    public readonly float Restitution;
    public readonly float Area;

    public readonly bool IsStatic;

    public readonly float Radius;
    public readonly float Width;
    public readonly float Height;

    public readonly ShapeType ShapeType;

    public Vector Position {
        get { return position; }
    }

    private Body(Vector position, float density, float mass, float restitution, float area, bool isStatic, float radius, float width, float height, ShapeType shapeType)
    {
        this.position = position;
        linearVelocity = Vector.Zero;
        rotation = 0f;
        rotationalVelocity = 0f;
        Density = density;
        Mass = mass;
        Restitution = restitution;
        Area = area;
        IsStatic = isStatic;
        Radius = radius;
        Width = width;
        Height = height;
        ShapeType = shapeType;
    }

    public static bool CreateCircleBody(float radius, Vector position, float density, bool isStatic, float restitution, out Body body, out string errorMessage)
    {
        body = null;
        errorMessage = string.Empty;

        float area = radius * radius * MathF.PI;

        if (area < World.MinBodySize) {
            errorMessage = $"Radius is too small. Min area is {World.MinBodySize}";
            return false;
        }

        if (area > World.MaxBodySize) {
            errorMessage = $"Radius is too large. Max area is {World.MaxBodySize}";
            return false;
        }

        if (density < World.MinDensity) {
            errorMessage = $"Density is too small. Min density is {World.MinDensity}";
            return false;
        }

        if (density > World.MaxDensity) {
            errorMessage = $"Density is too large. Max density is {World.MaxDensity}";
            return false;
        }

        restitution = Math.Clamp(restitution, 0f, 1f);

        // mass = area * depth * density
        float mass = area * density;

        body = new Body(position, density, mass, restitution, area, isStatic, radius, 0f, 0f, ShapeType.Circle);

        return true;
    }

    public static bool CreateBoxBody(float width, float height, Vector position, float density, bool isStatic, float restitution, out Body body, out string errorMessage)
    {
        body = null;
        errorMessage = string.Empty;

        float area = width * height;

        if (area < World.MinBodySize) {
            errorMessage = $"Area is too small. Min area is {World.MinBodySize}";
            return false;
        }

        if (area > World.MaxBodySize) {
            errorMessage = $"Area is too large. Max area is {World.MaxBodySize}";
            return false;
        }

        if (density < World.MinDensity) {
            errorMessage = $"Density is too small. Min density is {World.MinDensity}";
            return false;
        }

        if (density > World.MaxDensity) {
            errorMessage = $"Density is too large. Max density is {World.MaxDensity}";
            return false;
        }

        restitution = Math.Clamp(restitution, 0f, 1f);

        // mass = area * depth * density
        float mass = area * density;

        body = new Body(position, density, mass, restitution, area, isStatic, 0f, width, height, ShapeType.Box);

        return true;
    }
}
