using On.RoR2.UI.LogBook;
using RoR2;
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
using System.Reflection;
using System;
using RoR2.UI.LogBook;
using RoR2.ExpansionManagement;
using System.Collections.Generic;
using RoR2.EntitlementManagement;
using System.Linq;

namespace EnemyTemplateMod.Modules
{
    internal class EnemyBase : ScriptableObject
    {
		public GameObject CharacterMaster;

		// Token: 0x0400002E RID: 46
		public GameObject CharacterBody;

		// Token: 0x0400002F RID: 47
		public GameObject CharacterModel;

		// Token: 0x04000030 RID: 48
		public string Name;

		// Token: 0x04000031 RID: 49
		public UnlockableDef logUnlock;

		// Token: 0x04000032 RID: 50
		[Header("Skills and Projectiles")]
		public BuffDef[] Buffs;

		// Token: 0x04000033 RID: 51
		public SkillFamily[] SkillFamilies;

		// Token: 0x04000034 RID: 52
		public SkillDef[] Skills;

		// Token: 0x04000035 RID: 53
		public GameObject[] Effects;

		// Token: 0x04000036 RID: 54
		public GameObject[] Projectiles;

		// Token: 0x04000037 RID: 55
		public GameObject[] MiscGameObjects;

		// Token: 0x04000038 RID: 56
		[Header("Spawn Card")]
		public CharacterSpawnCard CharacterSpawnCard;

		// Token: 0x04000039 RID: 57
		public string CharacterCategory = "";

		// Token: 0x0400003A RID: 58
		public int SelectionWeight = 1;

		// Token: 0x0400003B RID: 59
		public DirectorCore.MonsterSpawnDistance spawnDistance;

		// Token: 0x0400003C RID: 60
		public int MinimumStageCompletion;
	}
}
