using System;
using System.Collections.Generic;

namespace BF3MapListGenerator
{
    [Flags] public enum GameTypes : int
    {
        /// <summary>
        /// A list of game types that a map can belong to.
        /// This is used as a flag during map list population.
        /// </summary>
        NONE = 0,
        CONQUESTASSAULTLARGE0 = 1, // Up to 64 players
        CONQUESTASSAULTSMALL0 = 2, // Up to 32 players
        CONQUESTASSAULTSMALL1 = 4, // Up to 32 players
        CONQUESTLARGE0 = 8, // Up to 64 players
        CONQUESTSMALL0 = 16, // Up to 32 players
        RUSHLARGE0 = 32, // Up to 32 players
        SQUADDEATHMATCH0 = 64, // Up to 16 players
        SQUADRUSH0 = 128, // Up to 8 players
        TEAMDEATHMATCH0 = 256, // Up to 24 players
        TEAMDEATHMATCHC0 = 512, // Up to 16 players - Close Quarters
        GUNMASTER0 = 1024, // Up to 16 players - Close Quarters
        DOMINATION0 = 2048, // Up to 16 players - Close Quarters
        TANKSUPERIORITY0 = 4096, // Up to 24 players - Armored Kill
        SCAVENGER0 = 8192, // Up to 32 players - Aftermath
        AIRSUPERIORITY0 = 16384, // Up to 24 players - End Game
        CAPTURETHEFLAG0 = 32768, // Up to 32 players - End Game
        // Compound enums
        BASE = SQUADDEATHMATCH0 | TEAMDEATHMATCH0 | SQUADRUSH0 | RUSHLARGE0 | CONQUESTSMALL0 | CONQUESTLARGE0 | TEAMDEATHMATCHC0,
        EXPANSION1 = SQUADDEATHMATCH0 | TEAMDEATHMATCH0 | SQUADRUSH0 | RUSHLARGE0 | CONQUESTASSAULTSMALL0 | CONQUESTASSAULTSMALL1 | CONQUESTASSAULTLARGE0 | TEAMDEATHMATCHC0,
        EXPANSION2 = SQUADDEATHMATCH0 | TEAMDEATHMATCHC0 | GUNMASTER0 | DOMINATION0,
        EXPANSION3 = BASE | TANKSUPERIORITY0,
        EXPANSION4 = BASE | GUNMASTER0 | SCAVENGER0,
        EXPANSION5 = BASE | CAPTURETHEFLAG0 | AIRSUPERIORITY0,
        B2KCONQUESTLARGE = CONQUESTASSAULTLARGE0 | CONQUESTLARGE0,
        B2KCONQUESTSMALL = CONQUESTASSAULTSMALL0 | CONQUESTASSAULTSMALL1 | CONQUESTSMALL0,
    }

    public class BF3Map : ICloneable
    {
        /// <summary>
        /// A datastructure describing a BF3 Map
        /// </summary>
        public string InternalName;
        public string FriendlyName;
        // public string XPackName; // Expansion Pack Name
        public GameTypes GameTypeList = GameTypes.NONE;
        public int XPack;
        public BF3GameType IntendedGameType = new BF3GameType();
        // Default constructor
        public BF3Map() { }
        public BF3Map( string iname, string fname, int xp, GameTypes gts )
        {
            InternalName = iname;
            FriendlyName = fname;
            XPack = xp;
            GameTypeList = gts;
        }
        /// <summary>
        /// Clones the object to prevent the Program.BF3Maps reference from being updated
        /// when generating map lists for each game type.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            BF3Map b = new BF3Map();
            b.InternalName = this.InternalName;
            b.FriendlyName = this.FriendlyName;
            b.GameTypeList = this.GameTypeList;
            b.IntendedGameType = this.IntendedGameType;
            return b;
        }
    }

    public class BF3GameType
    {
        /// <summary>
        /// A data structure describing a BF3 Game Type
        /// </summary>
        public string InternalName; // Internal game type name
        public string FriendlyName;
        public int NumPlayers = -1; // Number of players supported by game type - THIS PROPERTY IS NOT USED RIGHT NOW.
        public GameTypes GameType = GameTypes.NONE;
        // Default constructor
        public BF3GameType() { }
        public BF3GameType( string iname, string fname, int nump, GameTypes gt )
        {
            InternalName = iname;
            FriendlyName = fname;
            NumPlayers = nump;
            GameType = gt;
        }
    }

    public class BF3MapListPerGameType
    {
        /// <summary>
        /// This class describes a map list per game type pattern.
        /// It is populated from the pattern listbox lbGameTypesPattern on MainForm.
        /// </summary>
        /// 
        /// This class is instantiated via the MainForm.GenerateList() function.
        public GameTypes GameTypeEnum = GameTypes.NONE; // Gametype of the pattern entry
        public List<BF3Map> Maps = new List<BF3Map>();
        public decimal Rounds = -1;
        public List<BF3Map> UsedMaps = new List<BF3Map>();
        public int UsedMapsCurrentIndex = 0;
        // Default constructor
        public BF3MapListPerGameType() { }
        public BF3MapListPerGameType( GameTypes gt, decimal r )
        {
            GameTypeEnum = gt;
            Rounds = r;
        }
    }
}