using BepInEx;
using R2API.Utils;
using RoR2;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
namespace HenryAPI
{
    //[BepInDependency("com.rune580.riskofoptions", BepInDependency.DependencyFlags.SoftDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, MODNAME, MODVERSION)]
    public class HenryAPIPlugin : BaseUnityPlugin
    {
        public const string MODUID = "com.buns.HenryAPI";
        public const string MODNAME = "HenryAPI";
        public const string MODVERSION = "1.0.0";
        public const string DEVELOPER_PREFIX = "BUNS";

        public static HenryAPIPlugin instance;

        void Awake()
        {
            instance = this;

            //easy to use logger
            Log.Init(Logger);

            // used when you want to properly set up language folders
            Modules.Language.Init();
        }
    }
}
