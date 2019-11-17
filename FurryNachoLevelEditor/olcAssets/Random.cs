/************************************************************************************************
*                                    Acknowledgment                                             *
* This is straight up borrowed code from devChrome's port of Javidx9 olcPixelGameEngine         *
* https://github.com/DevChrome/Pixel-Engine                                                     *
* https://github.com/OneLoneCoder/olcPixelGameEngine                                            *
* This project is only intended as a hobby project, just for fun                                *
*************************************************************************************************/

using System;

namespace FurryNachoLevelEditor.olcAssets
{
    internal static class Randoms
    {
        private static Random rnd = new Random();
        private static int seed;

        public static int Seed { get => seed; set { seed = value; rnd = new Random(value); } }

        static Randoms() => Seed = Environment.TickCount;

        public static byte RandomByte(int count) => RandomBytes(1)[0];

        public static byte[] RandomBytes(int count)
        {
            byte[] b = new byte[count];
            rnd.NextBytes(b);
            return b;
        }

        public static int RandomInt(int min, int max) => rnd.Next(min, max);

        public static float RandomFloat(float min = 0, float max = 1) => (float)rnd.NextDouble() * (max - min) + min;
    }
}
