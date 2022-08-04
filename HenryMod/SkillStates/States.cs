using HenryMod.Modules.EnemiesDeath;
using HenryMod.Modules.EnemiesSpawn;

namespace HenryMod.SkillStates
{
    public static class States
    {
        internal static void RegisterStates()
        {
            Modules.Content.AddEntityState(typeof(EmptySkill));
            Modules.Content.AddEntityState(typeof(ShootOrb));
            Modules.Content.AddEntityState(typeof(BlinkPuppyBlink));

            Modules.Content.AddEntityState(typeof(DeathState));
            Modules.Content.AddEntityState(typeof(GunPupDeath));

            Modules.Content.AddEntityState(typeof(SpawnState));
            Modules.Content.AddEntityState(typeof(GunPupSpawn));
        }
    }
}