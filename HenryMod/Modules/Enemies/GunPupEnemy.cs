using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using HenryMod.SkillStates;
using R2API;
using UnityEngine.AddressableAssets;
using EntityStates;
using static HenryMod.Modules.Prefabs;
using HenryMod.Modules.Characters;
using HenryMod.Modules.EnemiesDeath;
using HenryMod.Modules.EnemiesSpawn;

namespace HenryMod.Modules.Enemies
{
    public static class GunPupEnemy
    {
        internal static GameObject characterPrefab;
        public static int bodyRendererIndex;
        internal static GameObject basicEnemyMaster;
        internal static int spawnCost = 25;
        internal static int minimumStageCount = 0;

        internal static int moneyEarned = 10;

        internal static void CreateCharacter()
        {
            LanguageAPI.Add("MINTBEEPS_GUNPUPBASIC_BODY_NAME", "Gun Puppy");
            LanguageAPI.Add("MINTBEEPS_GUNPUPBASIC_BODY_SUBTITLE", "The Clockwork Cannon");

            GunPupEnemy.characterPrefab = Prefabs.CreateBodyPrefab("MGunPupB", "enmGunPupBasic", new BodyInfo
            {
                moveSpeed = 0f,
                acceleration = 0f,
                armor = 0f,
                armorGrowth = 0f,
                bodyName = "MGunPupB",
                bodyNameToken = "MINTBEEPS_GUNPUPBASIC_BODY_NAME",
                bodyColor = Color.white,
                characterPortrait = Assets.mainAssetBundle.LoadAsset<Texture>("texHenryIcon"),
                crosshair = Modules.Assets.LoadCrosshair("Standard"),
                damage = 12f,
                healthGrowth = 45f,
                healthRegen = 0f,
                jumpCount = 0,
                maxHealth = 100f,
                subtitleNameToken = "MINTBEEPS_GUNPUPENEMY_BODY_SUBTITLE",
                podPrefab = Resources.Load<GameObject>("Prefabs/NetworkedObjects/SurvivorPod")
            });
            //Material baseEnMat = Assets.CreateMaterial("matGunPupBasic");
            //GunPupEnemy.bodyRendererIndex = 0;
            Prefabs.SetupCharacterModel(GunPupEnemy.characterPrefab, new CustomRendererInfo[]
            {
                new CustomRendererInfo
                {
                    childName = "GunBasic",
                    material = Materials.CreateHopooMaterial("matGunBasic")
                }
            });
            GunPupEnemy.CreateHitboxes();
            GunPupEnemy.CreateSkills();
            //GunPupEnemy.CreateSkins();
            //GunPupEnemy.InitializeItemDisplays();
            GunPupEnemy.basicEnemyMaster = (GameObject)CreateMaster(GunPupEnemy.characterPrefab, "BasicGunMaster");
            GunPupEnemy.CreateSpawnCard();

            ModelLocator component = characterPrefab.GetComponent<ModelLocator>();
            component.noCorpse = true;

            CharacterDeathBehavior component2 = characterPrefab.GetComponent<CharacterDeathBehavior>();
            component2.deathState = new SerializableEntityStateType(typeof(DeathState));

            // MUST SETNEXTSTATE TO MAIN OR ELSE THEY GET STUCK EXISTING
            //EntityStateMachine component3 = characterPrefab.GetComponent<EntityStateMachine>();
            //component3.initialStateType = new SerializableEntityStateType(typeof(SpawnState));
        }

        private static void CreateSpawnCard()
        {
            GunPupEnemy.spawnCost = 10;
            CharacterSpawnCard characterSpawnCard = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            characterSpawnCard.name = "cscGunPupBasic";
            characterSpawnCard.prefab = GunPupEnemy.basicEnemyMaster;
            characterSpawnCard.sendOverNetwork = true;
            characterSpawnCard.hullSize = 0;
            characterSpawnCard.nodeGraphType = 0;
            characterSpawnCard.requiredFlags = 0;
            characterSpawnCard.forbiddenFlags = RoR2.Navigation.NodeFlags.TeleporterOK;
            characterSpawnCard.directorCreditCost = GunPupEnemy.spawnCost;
            characterSpawnCard.occupyPosition = false;
            characterSpawnCard.loadout = new SerializableLoadout();
            characterSpawnCard.noElites = true;
            characterSpawnCard.forbiddenAsBoss = true;
            DirectorCard card = new DirectorCard
            {
                spawnCard = characterSpawnCard,
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = GunPupEnemy.minimumStageCount,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard//
            };
            //DirectorAPI.Helpers.AddNewMonsterToStage(card, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.WetlandAspect);
            //DirectorAPI.Helpers.AddNewMonsterToStage(card, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.TitanicPlains);
            //DirectorAPI.Helpers.AddNewMonsterToStage(card, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.DistantRoost);
            DirectorAPI.Helpers.AddNewMonster(card, DirectorAPI.MonsterCategory.BasicMonsters);
        }

        private static object CreateMaster(GameObject bodyPrefab, string masterName)
        {
            GameObject masterObject = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Lemurian/LemurianMaster.prefab").WaitForCompletion(), masterName, true);
            masterObject.GetComponent<CharacterMaster>().bodyPrefab = bodyPrefab;
            foreach (AISkillDriver aiskillDriver in masterObject.GetComponentsInChildren<AISkillDriver>())
            {
                GameObject.DestroyImmediate(aiskillDriver);
            }
            AISkillDriver aiSkillDriver2 = masterObject.AddComponent<AISkillDriver>();
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
        }

        private static void CreateHitboxes()
        {
            ChildLocator componentInChildren = GunPupEnemy.characterPrefab.GetComponentInChildren<ChildLocator>();
            GameObject gameObject = componentInChildren.gameObject;
        }

        #region Skills
        private static void CreateSkills()
        {
            Skills.CreateSkillFamilies(GunPupEnemy.characterPrefab);
            string devString = EnemyPlugin.DEVELOPER_PREFIX;
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
        }
        #endregion
    }
}