using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace CloneRoids
{
    public static class Constants
    {
        public const Keys ShootKey = Keys.Z;

        public const int PlayerSpeed = 300;
        public const int PlayerTurnSpeed = 2;

        public const int PlayerWidth = 20;
        public const int PlayerHeight = 30;

        public const int ShootRadius = 10;

        public const int AsteroidRadius = 20;

        public const int ScreenWidth = 1280;
        public const int ScreenHeight = 720;


        public const float ShootLifeSpan = 1.5f;

        public const int ShootSpeed = 500;

        public const int PlayerLayer = 7;
        public const int ShootLayer = 8;
        public const int AsteroidLayer = 9;
    }
}
