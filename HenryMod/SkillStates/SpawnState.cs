using EntityStates;
using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HenryMod.SkillStates
{
    public class SpawnState : BaseState
    {
        public GameObject effectPrefab = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Lemurian/SpawnLemurian.prefab").WaitForCompletion(), "spawnEffectGunPuppy");
        public override void OnEnter()
        {
            base.OnEnter();
            EffectManager.SimpleEffect(effectPrefab, transform.position, transform.rotation, true);
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}