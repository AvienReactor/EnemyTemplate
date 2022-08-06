using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using EnemyTemplateMod.SkillStates;
using R2API;
using UnityEngine.AddressableAssets;
using EntityStates;
using static EnemyTemplateMod.Modules.Prefabs;
using EnemyTemplateMod.Modules.Characters;
using EnemyTemplateMod.Modules.EnemiesDeath;
using EnemyTemplateMod.Modules.EnemiesSpawn;

namespace EnemyTemplateMod.Modules.Enemies
{
    public static class GunPupEnemy
    {
        /* Hey if you want to test to make sure your character is setup correctly,
         * open the command line in game (ctrl + alt + `).
         * type in or paste "spawn_body MGunPupB" no quotation marks -GunPuppyBasic is changed with your (enemyName) from below.
         */

        internal static GameObject characterPrefab;
        public static int bodyRendererIndex;
        internal static GameObject basicEnemyMaster;
        internal static int spawnCost = 25; //based on how much the ai is charged to spawn the enemy When low cost more present  early game/ When high cost more present late game
        internal static int minimumStageCount = 0;  //based on how many stages need to be completed before they spawn. 0 = from the start

        internal static string enemyPrefix = EnemyPlugin.DEVELOPER_PREFIX;
        internal static string enemyName = "MGunPupB";  //change to the name you want to give them
        internal static string enemyPrefabName = "enmGunPupBasic";  //change to the name you gave the prefab in unity. (Make sure the prefab name has "enm" like the example!)

        internal static string enemyNameToken = enemyPrefix + "_GUNPUPBASIC_BODY_NAME"; //change "GUNPUPBASIC" to the name you want
        internal static string enemyNameTokenDescription = "Gun Puppy"; //change to the Description you want

        internal static string enemySubtitleNameToken = enemyPrefix + "_GUNPUPBASIC_BODY_SUBTITLE";  //change "GUNPUPBASIC" to the name you want
        internal static string enemySubtitleNameTokenDescription = "The Clockwork Cannon";  //change to the Description you want

        internal static void CreateCharacter()
        {
            LanguageAPI.Add(enemyNameToken, enemySubtitleNameTokenDescription);
            LanguageAPI.Add(enemySubtitleNameToken, enemySubtitleNameTokenDescription);

            GunPupEnemy.characterPrefab = Prefabs.CreateBodyPrefab(enemyName, enemyPrefabName, new BodyInfo
            {
                #region Stats
                //do what you want for all these go wild
                moveSpeed = 0f,
                moveSpeedGrowth = 0f,
                acceleration = 0f,
                armor = 0f,
                armorGrowth = 0f,
                shield = 0f,
                shieldGrowth = 0f,
                bodyName = enemyName,   //keep this
                bodyNameToken = enemyNameToken, //keep this
                bodyColor = Color.white,
                characterPortrait = Assets.mainAssetBundle.LoadAsset<Texture>("texHenryIcon"),  //change to the icon name to the one in your unity project
                crosshair = Modules.Assets.LoadCrosshair("Standard"),
                damage = 12f,
                damageGrowth = 2.4f,
                attackSpeed = 1f,
                attackSpeedGrowth = 0f,
                crit = 0f,
                critGrowth = 0f,
                maxHealth = 100f,
                healthGrowth = 45f,
                healthRegen = 0f,
                regenGrowth = 0f,
                jumpCount = 0,
                jumpPower = 0f,
                jumpPowerGrowth = 0f,
                subtitleNameToken = enemySubtitleNameToken, //keep this
                podPrefab = Resources.Load<GameObject>("Prefabs/NetworkedObjects/SurvivorPod")
                #endregion
            });
            //Material baseEnMat = Assets.CreateMaterial("matGunPupBasic");
            //GunPupEnemy.bodyRendererIndex = 0;
            Prefabs.SetupCharacterModel(GunPupEnemy.characterPrefab, new CustomRendererInfo[]
            {
                #region RenderInfo
                new CustomRendererInfo  //for more than one mesh renderer being used duplicate this whole new CustomRenderInfo
                {
                    childName = "GunBasic", //this is found in the unity project inside the childlocator for the Mesh Renderer
                }
                #endregion
            });
            GunPupEnemy.CreateHitboxes();
            GunPupEnemy.CreateSkills();
            GunPupEnemy.basicEnemyMaster = (GameObject)CreateMaster(GunPupEnemy.characterPrefab, "BasicGunMaster"); //change "BasicGunMaster" to what you want the EnemyMaster called
            GunPupEnemy.CreateSpawnCard();

            ModelLocator component = characterPrefab.GetComponent<ModelLocator>();
            component.noCorpse = true;

            CharacterDeathBehavior component2 = characterPrefab.GetComponent<CharacterDeathBehavior>();
            component2.deathState = new SerializableEntityStateType(typeof(GunPupDeath));    //change GunPupDeath to your enemies death script

            EntityStateMachine component3 = characterPrefab.GetComponent<EntityStateMachine>();
            component3.initialStateType = new SerializableEntityStateType(typeof(GunPupSpawn));    //change GunPupSpawn to your enemies spawn script
        }

        private static void CreateSpawnCard()
        {
            CharacterSpawnCard characterSpawnCard = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            characterSpawnCard.name = "cscGunPupBasic"; //keep csc infront of the enemy name but change to your enemy name
            characterSpawnCard.prefab = GunPupEnemy.basicEnemyMaster;
            characterSpawnCard.sendOverNetwork = true;
            characterSpawnCard.hullSize = 0;
            characterSpawnCard.nodeGraphType = 0;
            characterSpawnCard.requiredFlags = 0;
            characterSpawnCard.forbiddenFlags = RoR2.Navigation.NodeFlags.TeleporterOK;
            characterSpawnCard.directorCreditCost = GunPupEnemy.spawnCost;
            characterSpawnCard.occupyPosition = false;
            characterSpawnCard.loadout = new SerializableLoadout();
            characterSpawnCard.noElites = true; //true = wont spawn as Elites / false = have a chance to spawn as Elites
            characterSpawnCard.forbiddenAsBoss = true;  //Keep this 
            DirectorCard card = new DirectorCard
            {
                spawnCard = characterSpawnCard,
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = GunPupEnemy.minimumStageCount,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            //DirectorAPI.Helpers.AddNewMonsterToStage(card, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.WetlandAspect);   //this spawns enemies on specific stages, just duplicate to add to more stages
            DirectorAPI.Helpers.AddNewMonster(card, DirectorAPI.MonsterCategory.BasicMonsters); //this spawns enemies on all stages
        }

        private static object CreateMaster(GameObject bodyPrefab, string masterName)
        {
            GameObject masterObject = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Lemurian/LemurianMaster.prefab").WaitForCompletion(), masterName, true);
            masterObject.GetComponent<CharacterMaster>().bodyPrefab = bodyPrefab;
            foreach (AISkillDriver aiskillDriver in masterObject.GetComponentsInChildren<AISkillDriver>())
            {
                GameObject.DestroyImmediate(aiskillDriver);
            }
            #region AISkillDrivers
            AISkillDriver aiSkillDriver2 = masterObject.AddComponent<AISkillDriver>();
            masterObject.GetComponent<BaseAI>().fullVision = true;  //true they always see you? or can see all around them / false it wont do that
            aiSkillDriver2.customName = "ShootOrb";
            aiSkillDriver2.movementType = AISkillDriver.MovementType.Stop;
            aiSkillDriver2.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            aiSkillDriver2.activationRequiresAimConfirmation = true;
            aiSkillDriver2.activationRequiresTargetLoS = true;
            aiSkillDriver2.selectionRequiresTargetLoS = true;
            aiSkillDriver2.maxDistance = 100f;
            aiSkillDriver2.minDistance = 1f;
            aiSkillDriver2.requireSkillReady = true;
            aiSkillDriver2.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            aiSkillDriver2.ignoreNodeGraph = false;
            aiSkillDriver2.moveInputScale = 1f;
            aiSkillDriver2.driverUpdateTimerOverride = 0.5f;
            aiSkillDriver2.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            aiSkillDriver2.minTargetHealthFraction = float.NegativeInfinity;
            aiSkillDriver2.maxTargetHealthFraction = float.PositiveInfinity;
            aiSkillDriver2.minUserHealthFraction = float.NegativeInfinity;
            aiSkillDriver2.maxUserHealthFraction = float.PositiveInfinity;
            aiSkillDriver2.skillSlot = SkillSlot.Primary;

            AISkillDriver aiSkillDriver3 = masterObject.AddComponent<AISkillDriver>();
            aiSkillDriver3.customName = "Follow";
            aiSkillDriver3.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            aiSkillDriver3.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            aiSkillDriver3.activationRequiresAimConfirmation = false;
            aiSkillDriver3.activationRequiresTargetLoS = false;
            aiSkillDriver3.selectionRequiresTargetLoS = false;
            aiSkillDriver3.maxDistance = 100f;
            aiSkillDriver3.minDistance = 1f;
            aiSkillDriver3.requireSkillReady = false;
            aiSkillDriver3.aimType = AISkillDriver.AimType.MoveDirection;
            aiSkillDriver3.ignoreNodeGraph = false;
            aiSkillDriver3.moveInputScale = 1f;
            aiSkillDriver3.driverUpdateTimerOverride = 0.5f;
            aiSkillDriver3.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            aiSkillDriver3.minTargetHealthFraction = float.NegativeInfinity;
            aiSkillDriver3.maxTargetHealthFraction = float.PositiveInfinity;
            aiSkillDriver3.minUserHealthFraction = float.NegativeInfinity;
            aiSkillDriver3.maxUserHealthFraction = float.PositiveInfinity;
            aiSkillDriver3.skillSlot = SkillSlot.None;
            Content.AddMasterPrefab(masterObject);
            return masterObject;
            #endregion
        }


        #region Skills
        private static void CreateSkills()
        {
            Skills.CreateSkillFamilies(GunPupEnemy.characterPrefab);
            string devString = EnemyPlugin.DEVELOPER_PREFIX;
            #region PrimarySkill
            SkillDef basicOrbSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = devString + "_GUNPUPENEMY_BODY_PRIMARY_NAME",
                skillNameToken = devString + "_GUNPUPENEMY_BODY_PRIMARY_NAME",
                skillDescriptionToken = devString + "_GUNPUPENEMY_BODY_PRIMARY_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texSecondaryIcon"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ShootOrb)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });
            Skills.AddPrimarySkills(characterPrefab, basicOrbSkillDef);
            #endregion

            #region SecondarySkill
            SkillDef emptySecSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = devString + "_GUNPUPENEMY_BODY_SECONDARY_NAME",
                skillNameToken = devString + "_GUNPUPENEMY_BODY_SECONDARY_NAME",
                skillDescriptionToken = devString + "_GUNPUPENEMY_BODY_SECONDARY_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texSecondaryIcon"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.EmptySkill)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });
            Skills.AddSecondarySkills(characterPrefab, emptySecSkillDef);
            #endregion

            #region UtilitySkill
            SkillDef emptyUtiSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = devString + "_GUNPUPENEMY_BODY_UTILITY_NAME",
                skillNameToken = devString + "_GUNPUPENEMY_BODY_UTILITY_NAME",
                skillDescriptionToken = devString + "_GUNPUPENEMY_BODY_UTILITY_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texSecondaryIcon"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.EmptySkill)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });
            Skills.AddUtilitySkills(characterPrefab, emptyUtiSkillDef);
            #endregion

            #region SpecalSkill
            SkillDef emptySpecSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = devString + "_GUNPUPENEMY_BODY_SPECIAL_NAME",
                skillNameToken = devString + "_GUNPUPENEMY_BODY_SPECIAL_NAME",
                skillDescriptionToken = devString + "_GUNPUPENEMY_BODY_SPECIAL_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texSecondaryIcon"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.EmptySkill)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });
            Skills.AddSpecialSkills(characterPrefab, emptySpecSkillDef);
            #endregion
        }
        #endregion

        private static void CreateHitboxes()    //Dont touch
        {
            ChildLocator componentInChildren = GunPupEnemy.characterPrefab.GetComponentInChildren<ChildLocator>();
            GameObject gameObject = componentInChildren.gameObject;
        }
    }
}