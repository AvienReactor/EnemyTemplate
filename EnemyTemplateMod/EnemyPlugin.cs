using BepInEx;
using EnemyTemplateMod.Modules;
using EnemyTemplateMod.Modules.Enemies;
using R2API.Utils;
using RoR2;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;

namespace EnemyTemplateMod
{
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, MODNAME, MODVERSION)]
    [R2APISubmoduleDependency(new string[]
    {
        "PrefabAPI",
        "LanguageAPI",
        "SoundAPI",
        "UnlockableAPI",
        "DirectorAPI"
    })]

    //public static Dictionary<string, EnemyBase> enemyBaseScriptableObjects = new Dictionary<string, EnemyBase>();

    public class EnemyPlugin : BaseUnityPlugin
    {
        // if you don't change these you're giving permission to deprecate the mod-
        //  please change the names to your own stuff, thanks
        //   this shouldn't even have to be said
        public const string MODUID = "com.Lemurians.EnemyTemplateMod";
        public const string MODNAME = "EnemyTemplate";
        public const string MODVERSION = "1.0.0";

        // a prefix for name tokens to prevent conflicts- please capitalize all name tokens for convention
        public const string DEVELOPER_PREFIX = "LEMURIANS";
        public const string assetbundleName = "gunpupassetbundle";    //name the same as your assetbundle made in Unity
        public const string csProjName = "EnemyTemplateMod";

        public static EnemyPlugin instance;

        private void Awake()
        {
            instance = this;

            Log.Init(Logger);
            Modules.Assets.Initialize(); // load assets and read config
            //Modules.Config.ReadConfig();
            SkillStates.States.RegisterStates(); // register states for networking
            //Modules.Buffs.RegisterBuffs(); // add and register custom buffs/debuffs
            Modules.Projectiles.RegisterProjectiles(); // add and register custom projectiles
            Modules.Tokens.AddTokens(); // register name tokens

            // enemy initialization
            GunPupEnemy.CreateCharacter();

            //log enties
            

            // now make a content pack and add it- this part will change with the next update
            new Modules.ContentPacks().Initialize();

            Hook();
        }

        private void Hook()
        {
            // run hooks here, disabling one is as simple as commenting out the line

        }
    }
}
