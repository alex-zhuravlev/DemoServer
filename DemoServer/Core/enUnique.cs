using System;

namespace DemoServer.Core
{
    public static class enUnique
    {
        private static ulong s_iIdCounter = 1000;

        public static ulong GenerateId()
        {
            return s_iIdCounter++;
        }
    }
}