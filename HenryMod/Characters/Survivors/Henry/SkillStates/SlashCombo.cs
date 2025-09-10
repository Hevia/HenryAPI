using HenryMod.Modules.BaseStates;
using RoR2.Skills;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace HenryMod.Survivors.Henry.SkillStates
{
    public class SlashCombo : BaseMeleeAttack
    {
        public override void OnEnter()
        {
            //mouse over variables for detailed explanations
            hitBoxGroupName = "SwordGroup";

            damageType = DamageTypeCombo.GenericPrimary;
            damageCoefficient = HenryContent.CharacterStaticValues.swordDamageCoefficient;
            procCoefficient = 1f;
            pushForce = 300f;
            bonusForce = Vector3.zero;
            baseDuration = 1f;

            //0-1 multiplier of baseduration, used to time when the hitbox is out (usually based on the run time of the animation)
            attackStartTimeFraction = 0.2f;
            attackEndTimeFraction = 0.4f;

            earlyExitTimeFraction = 0.6f;

            hitStopDuration = 0.012f;
            attackRecoil = 0.5f;
            hitHopVelocity = 4f;

            swingSoundString = "HenrySwordSwing";
            playbackRateParam = "Slash.playbackRate";
            //swingIndex is set by the base class being a steppedSkillDef. See SlashComboTriple for more detail
            muzzleString = swingIndex == 0 ? "SwingLeft" : "SwingRight";
            swingEffectPrefab = HenryContent.CharacterAssets.swordSwingEffect;
            hitEffectPrefab = HenryContent.CharacterAssets.swordHitImpactEffect;

            impactSound = HenryContent.CharacterAssets.swordHitSoundEvent.index;

            base.OnEnter();

            PlayAttackAnimation();
        }

        protected override void PlayAttackAnimation()
        {
            //play a adifferent animation based on what step of the combo you are currently in.
            if (swingIndex == 0)
            {
                PlayCrossfade("Gesture, Override", "Slash1", playbackRateParam, duration, 0.1f * duration);
            }
            if (swingIndex == 1)
            {
                PlayCrossfade("Gesture, Override", "Slash2", playbackRateParam, duration, 0.1f * duration);
            }
            //as a challenge, see if you can rewrite this code to be one line.
        }

        protected override void PlaySwingEffect()
        {
            base.PlaySwingEffect();
        }

        protected override void OnHitEnemyAuthority()
        {
            base.OnHitEnemyAuthority();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}