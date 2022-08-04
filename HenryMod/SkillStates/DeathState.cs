using System;
using EntityStates;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace HenryMod.SkillStates
{
    public class DeathState : GenericCharacterDeath
    {
        public uint moneyEarned { get; set; }

        public override void OnEnter()
        {
            base.OnEnter();
            bool flag = modelLocator;
            if (flag)
            {
                bool flag2 = modelLocator.modelBaseTransform;
                if (flag2)
                {
                    Destroy(modelLocator.modelBaseTransform.gameObject);
                }
                bool flag3 = modelLocator.modelTransform;
                if (flag3)
                {
                    Destroy(modelLocator.modelTransform.gameObject);
                }
            }
            bool active = NetworkServer.active;
            if (active)
            {
                EffectManager.SimpleEffect(initialExplosion, transform.position, transform.rotation, true);
                Destroy(gameObject);
                TeamManager.instance.GiveTeamMoney(TeamIndex.Player, moneyEarned);
            }
        }

        // Token: 0x04000001 RID: 1
        public static GameObject initialExplosion = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Common/VFX/OmniExplosionVFXDroneDeath.prefab").WaitForCompletion();
    }
}
