using EnemyTemplateMod.Modules.EnemiesDeath;
using EnemyTemplateMod.Modules.EnemiesSpawn;

namespace EnemyTemplateMod.SkillStates
{
    public static class States
    {
        internal static void RegisterStates()
        {
            Modules.Content.AddEntityState(typeof(EmptySkill));
            Modules.Content.AddEntityState(typeof(ShootOrb));
            Modules.Content.AddEntityState(typeof(BlinkPuppyBlink));

            Modules.Content.AddEntityState(typeof(DeathStateBase));
            Modules.Content.AddEntityState(typeof(GunPupDeath));

            Modules.Content.AddEntityState(typeof(SpawnStateBase));
            Modules.Content.AddEntityState(typeof(GunPupSpawn));
        }
    }
}