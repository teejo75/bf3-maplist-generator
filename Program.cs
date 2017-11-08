using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BF3MapListGenerator
{
    public static class Program
    {
        #region PROGRAM_STARTUP_CODE
        /// <summary>
        /// Populates a list of class BF3GameType with game type data
        /// </summary>
        /// 
        const int XPACK_VANILLA = 0, XPACK_B2K = 1, XPACK_CQ = 2, XPACK_AK = 3, XPACK_AM = 4, XPACK_EG = 5;
        public static string[] BF3XPack = { "Vanilla", "Back to Karand", "Close Quarters", "Armored Kill", "Aftermath", "End Game" };

        public static List<BF3GameType> bf3GameTypes = new List<BF3GameType>() {
            new BF3GameType( "ConquestAssaultLarge0", "Assault 64", 64, GameTypes.CONQUESTASSAULTLARGE0 ),
            new BF3GameType( "ConquestAssaultSmall0","Assault",32, GameTypes.CONQUESTASSAULTSMALL0 ),
            new BF3GameType( "ConquestAssaultSmall1","Assault #2",32, GameTypes.CONQUESTASSAULTSMALL1 ),
            new BF3GameType( "ConquestLarge0","Conquest Large",64, GameTypes.CONQUESTLARGE0 ),
            new BF3GameType( "ConquestSmall0","Conquest Small",32, GameTypes.CONQUESTSMALL0 ),
            new BF3GameType( "RushLarge0","Rush",32, GameTypes.RUSHLARGE0 ),
            new BF3GameType( "SquadRush0","Squad Rush",8, GameTypes.SQUADRUSH0 ),
            new BF3GameType( "SquadDeathMatch0","Squad Deathmatch",16, GameTypes.SQUADDEATHMATCH0 ),
            new BF3GameType( "TeamDeathMatch0","Team Deathmatch",24, GameTypes.TEAMDEATHMATCH0 ),
            new BF3GameType( "B2KConquestLarge","B2K Conquest Large",64,GameTypes.B2KCONQUESTLARGE ),
            new BF3GameType( "B2KConquestSmall", "B2K Conquest Small",32,GameTypes.B2KCONQUESTSMALL ),
            new BF3GameType( "TeamDeathMatchC0","TDM Close Quarters",16,GameTypes.TEAMDEATHMATCHC0 ),
            new BF3GameType( "GunMaster0","Gun Master",16,GameTypes.GUNMASTER0 ),
            new BF3GameType( "Domination0","Conquest Domination",16,GameTypes.DOMINATION0 ),
            new BF3GameType( "TankSuperiority0","Tank Superiority",32,GameTypes.TANKSUPERIORITY0 ),
            new BF3GameType( "Scavenger0", "Scavenger",32,GameTypes.SCAVENGER0 ),
            new BF3GameType( "AirSuperiority0", "Air Superiority",24,GameTypes.AIRSUPERIORITY0 ),
            new BF3GameType( "CaptureTheFlag0", "Capture The Flag",32,GameTypes.CAPTURETHEFLAG0 ),
            };

        /// <summary>
        /// Populates a list of class BF3Map with map data
        /// </summary>
        public static List<BF3Map> bf3Maps = new List<BF3Map>() 
        {
            // Constructor Internal Name, Friendly Name, Expansion Pack, Game Types enum
            new BF3Map( "MP_001","Grand Bazaar", XPACK_VANILLA, GameTypes.BASE ),
            new BF3Map( "MP_003","Teheran Highway", XPACK_VANILLA, GameTypes.BASE ),
            new BF3Map( "MP_007","Caspian Border", XPACK_VANILLA, GameTypes.BASE ),
            new BF3Map( "MP_011","Seine Crossing", XPACK_VANILLA, GameTypes.BASE ),
            new BF3Map( "MP_012","Operation Firestorm", XPACK_VANILLA, GameTypes.BASE ),
            new BF3Map( "MP_013","Damavand Peak", XPACK_VANILLA, GameTypes.BASE ),
            new BF3Map( "MP_017","Noshahr Canals", XPACK_VANILLA, GameTypes.BASE ),
            new BF3Map( "MP_018","Kharg Island", XPACK_VANILLA, GameTypes.BASE ),
            new BF3Map( "MP_Subway","Operation Metro", XPACK_VANILLA, GameTypes.BASE ),
            new BF3Map( "XP1_002","Gulf of Oman", XPACK_B2K, GameTypes.BASE | GameTypes.CONQUESTASSAULTSMALL0 ),
            new BF3Map( "XP1_001","Strike at Karkand", XPACK_B2K, GameTypes.EXPANSION1 ),
            new BF3Map( "XP1_003","Sharqi Peninsula", XPACK_B2K, GameTypes.EXPANSION1 ),
            new BF3Map( "XP1_004", "Wake Island", XPACK_B2K, GameTypes.EXPANSION1 ),
            new BF3Map( "XP2_Factory", "Scrap Metal", XPACK_CQ, GameTypes.EXPANSION2 ),
            new BF3Map( "XP2_Office", "Operation 925", XPACK_CQ, GameTypes.EXPANSION2 ),
            new BF3Map( "XP2_Palace", "Donya Fortress", XPACK_CQ, GameTypes.EXPANSION2 ),
            new BF3Map( "XP2_Skybar", "Ziba Tower", XPACK_CQ, GameTypes.EXPANSION2 ),
            new BF3Map( "XP3_Desert", "Bandar Desert", XPACK_AK, GameTypes.EXPANSION3 ),
            new BF3Map( "XP3_Alborz", "Alborz Mountains", XPACK_AK, GameTypes.EXPANSION3 ),
            new BF3Map( "XP3_Shield", "Armored Shield", XPACK_AK, GameTypes.EXPANSION3 ),
            new BF3Map( "XP3_Valley", "Death Valley", XPACK_AK, GameTypes.EXPANSION3 ),
            new BF3Map( "XP4_FD", "Markaz Monolith", XPACK_AM, GameTypes.EXPANSION4 ),
            new BF3Map( "XP4_Parl", "Azadi Palace", XPACK_AM, GameTypes.EXPANSION4 ),
            new BF3Map( "XP4_Quake", "Epicenter", XPACK_AM, GameTypes.EXPANSION4 ),
            new BF3Map( "XP4_Rubble", "Talah Market", XPACK_AM, GameTypes.CONQUESTASSAULTLARGE0 | GameTypes.CONQUESTASSAULTSMALL0 | GameTypes.RUSHLARGE0 | GameTypes.SQUADRUSH0 | GameTypes.SQUADDEATHMATCH0 | GameTypes.TEAMDEATHMATCH0 | GameTypes.TEAMDEATHMATCHC0 | GameTypes.GUNMASTER0 | GameTypes.SCAVENGER0 ),
            new BF3Map( "XP5_001", "Operation Riverside", XPACK_EG, GameTypes.EXPANSION5 ),
            new BF3Map( "XP5_002", "Nebandan Flats", XPACK_EG, GameTypes.EXPANSION5 ),
            new BF3Map( "XP5_003", "Kiasar Railroad", XPACK_EG, GameTypes.EXPANSION5 ),
            new BF3Map( "XP5_004", "Sabalan Pipeline", XPACK_EG, GameTypes.EXPANSION5 ),
        };

        public static BF3Map SelectMap( List<BF3Map> maps, int rndindex ) //, int totalWeight)
        {

            BF3Map selectedMap = null;
            return selectedMap = maps[rndindex];
        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run(new MainForm());
        }
    }
}