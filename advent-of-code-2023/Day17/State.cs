using System.Numerics;

namespace advent_of_code_2023.Day17;
internal struct State
{
    public readonly Vector2 Position;
    public readonly Vector2 Direction;
    public readonly int StepsInDirection;

    public State(Vector2 position, Vector2 direction, int stepsInDirection)
    {
        Position = position;
        Direction = direction;
        StepsInDirection = stepsInDirection;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj is not State other) return false;

        return 
            this.Position == other.Position &&
            this.Direction == other.Direction &&
            this.StepsInDirection == other.StepsInDirection;
    }

    public override int GetHashCode()
    {
        return Position.GetHashCode()
            ^ Direction.GetHashCode()
            ^ StepsInDirection.GetHashCode();
    }

    public override string ToString() =>
        $"({Position.X},{Position.Y}), {Direction.ToDirectionName()}, {StepsInDirection}";
}
