using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Godot.TextServer;

namespace Frogger.Script
{
    public static class EnemyVelocityManager
    {
        public static float SetEnemyVelocity(float _direction)
        {
          return (0.5f + Victory.VelocityIncrease()) * _direction;
        }

        public static float SetEnemyRotation(float _direction)
        {
            if (_direction == -1)
            {
                return 180;
            }
            return 0;
        }

        public static Vector2 MoveEnemy(Vector2 _globalPosition,float _velocity)
        {
            return new Vector2(_globalPosition.X - _velocity + Victory.VelocityIncrease(), _globalPosition.Y);
        }
        public static Vector2 MoveWaterEnemy(Vector2 _globalPosition,float _velocity)
        {
            return new Vector2(_globalPosition.X - _velocity, _globalPosition.Y);
        }


    }
}
