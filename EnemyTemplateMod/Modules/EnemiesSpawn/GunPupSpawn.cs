using EntityStates;
using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using EnemyTemplateMod.SkillStates;

namespace EnemyTemplateMod.Modules.EnemiesSpawn
{
    internal class GunPupSpawn : SpawnStateBase
    {
        public override void OnEnter()
        {
            base.OnEnter();
            effectPrefab = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Lemurian/SpawnLemurian.prefab").WaitForCompletion(), "spawnEffectGunPuppy");
            EffectManager.SimpleEffect(effectPrefab, transform.position, transform.rotation, true);
        }

        public override void OnExit()
        {
            base.OnExit();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.isAuthority)
            {
                this.outer.SetNextStateToMain();    //NEED TO HAVE THIS
            }
        }

    }
}
