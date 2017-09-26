﻿namespace MsilDecompiler.Host
{
    public static class MsilDecompilerEndpoints
    {
        public const string AddAssembly = "/addassembly";
        public const string DecompileAssembly = "/decompileassembly";
        public const string ListTypes = "/listtypes";
        public const string DecompileType = "/decompiletype";
        public const string ListMembers = "/listmembers";
        public const string DecompileMember = "/decompilemember";
        public const string StopServer = "/stopserver";
    }
}