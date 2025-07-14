using Godot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;

public static partial class DeSpawner 
{
    public static List<int> DeletedAssetNumbers = new List<int>();

    public static void DeSpawnAsset(Node2D _node)
    {
        if (_node.GlobalPosition.X < -800 || _node.GlobalPosition.X > 1600)
        {
            if (_node.Name.ToString().Contains("Log"))
            { 
                getDeletedAssetNumber(_node.Name);
            }
            _node.QueueFree();
        }
    }

    public static void  getDeletedAssetNumber(string _name)
    {
        int id;
        id = Convert.ToInt32(_name.Substring(3));

        DeletedAssetNumbers.Add(id);
    }
}
