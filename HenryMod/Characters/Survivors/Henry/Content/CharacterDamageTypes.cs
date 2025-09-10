using R2API;

namespace HenryMod.Survivors.Henry.HenryContent
{
    public class CharacterDamageTypes
    {
        public static DamageAPI.ModdedDamageType comboFinisherDebuffDamage;

        public static void Init()
        {
            comboFinisherDebuffDamage = DamageAPI.ReserveDamageType();
        }
    }
}
