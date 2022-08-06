using RoR2;
using RoR2.ExpansionManagement;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.AddressableAssets;

namespace EnemyTemplateMod.Modules
{
    internal class Util
    {
		internal static bool ExpansionsEnabled()
		{
			bool flag = Run.instance.IsExpansionEnabled(Addressables.LoadAssetAsync<ExpansionDef>("RoR2/DLC1/Common/DLC1.asset").WaitForCompletion());
			bool flag2 = Run.instance.IsExpansionEnabled(Prefabs.expansionDef);
			bool flag3 = !flag || !flag2;
			return !flag3;
		}
	}
}
