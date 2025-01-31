using Godot;
using System;

public static partial class DeSpawner 
{
    public static void DeSpawnAsset(Node2D node)
    {
        if (node.GlobalPosition.X < -200 || node.GlobalPosition.X > 750)
        {
            node.QueueFree();
        }
    }
}
