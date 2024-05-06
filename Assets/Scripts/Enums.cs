using UnityEngine;

namespace Enums
{
    public enum CarMode
    {
        GROUND,
        WATER,
        AIR
    }

    public static class CarModeExtensions
    {
        public static Color GetColor(this CarMode mode)
        {
            switch (mode)
            {
                case CarMode.GROUND:
                    return Color.green;
                case CarMode.WATER:
                    return Color.blue;
                case CarMode.AIR:
                    return Color.yellow;
                default:
                    return Color.white;
            }
        }

        public static string ToString(this CarMode mode)
        {
            switch (mode)
            {
                case CarMode.GROUND:
                    return "Ground";
                case CarMode.WATER:
                    return "Water";
                case CarMode.AIR:
                    return "Air";
                default:
                    return "Unknown";
            }
        }
    }
}
