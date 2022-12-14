using System;
using EntityStates;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using EnemyTemplateMod.SkillStates;

namespace EnemyTemplateMod.Modules.EnemiesDeath
{
    internal class GunPupDeath : DeathStateBase
    {
        public override void OnEnter()
        {
            base.OnEnter();

            moneyEarned = 3;    //set how much money you want to earn when they die
            experienceEarned = 3;   //set how much exp you want to earn when they die
            initialExplosion = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Common/VFX/OmniExplosionVFXDroneDeath.prefab").WaitForCompletion();   //death vfx

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
                TeamManager.instance.GiveTeamMoney(TeamIndex.Player, moneyEarned *(uint)((base.characterBody.level)*1.2));  //the value 1.2 can be changed
                TeamManager.instance.GiveTeamExperience(TeamIndex.Player, experienceEarned * (ulong)((base.characterBody.level)*1.2));  //the value 1.2 can be changed
            }
        }
    }
}
