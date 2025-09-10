using HenryMod.Modules.BaseStates;
using RoR2.Skills;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using R2API;

namespace HenryMod.Survivors.Henry.SkillStates
{
    /// <summary>
    /// This is an example of a melee attack that uses a SteppedSkillDef to have different behavior.
    /// <see cref="HenrySurvivor.AddPrimarySkills"/> is where we create the skill as a SteppedSkillDef. Without going into too much detail, when you set up the SkillDef like this, that will run some code we can use here as explained below.
    /// Disclaimer, all the code is valid, but Henry's hitboxes and muzzles are not set up to use this attack. It is an example to use for your own characters
    /// </summary>
    public class SlashComboTriple : BaseMeleeAttack, SteppedSkillDef.IStepSetter
    {
        //the following field and function exist in base class, but let's just do it again here for tutorial purposes
        /// <summary>
        /// This is the index of the swing, according to the combo you are performing
        /// </summary>
        public int swingIndex;
        /// <summary>
        /// Each time you perform this skill, the SteppedSkillDef will call this function, and increment the swingIndex
        /// </summary>
        public void SetStep(int i)
        {
            swingIndex = i;
        }

        /// <summary>
        /// Convenience property that will let us check if we're at the last hit of the combo.
        /// </summary>
        public bool isComboFinisher => swingIndex == 2; //first hit is 0, so the third hit is 2

        public override void OnEnter()
        {
            // combo finisher lasts a little longer
            baseDuration = isComboFinisher ? 2 : 1f;

            attackStartTimeFraction = 0.2f;
            attackEndTimeFraction = 0.4f;

            earlyExitTimeFraction = 0.6f;

            //check combo finisher to change the attack hitbox. you'll have to set it up on your body and register it in your survivor setup code
            hitBoxGroupName = isComboFinisher ? "SwordBigGroup" : "SwordGroup";

            damageType = DamageTypeCombo.GenericPrimary;
            // combo fnisher has more damage
            damageCoefficient = isComboFinisher ? HenryContent.CharacterStaticValues.swordFinisherDamageCoefficient : HenryContent.CharacterStaticValues.swordDamageCoefficient;
            procCoefficient = 1f;
            pushForce = 300f;
            bonusForce = Vector3.zero;

            //combo finisher has a bit meatier hitstop. you get the pattern by now
            hitStopDuration = isComboFinisher ? 0.1f : 0.012f;
            attackRecoil = 0.5f;
            hitHopVelocity = 4f;

            //congratulations, you now are a master of the ternary (?) operator (or you will be in a second when you look it up right now)
            swingSoundString = isComboFinisher ? "HenrySwordSwingEpic" : "HenrySwordSwing";
            playbackRateParam = "Slash.playbackRate";
            muzzleString = GetComboMuzzle();
            swingEffectPrefab = HenryContent.CharacterAssets.swordSwingEffect;
            hitEffectPrefab = HenryContent.CharacterAssets.swordHitImpactEffect;

            impactSound = HenryContent.CharacterAssets.swordHitSoundEvent.index;

            //this performs more initialization of the melee attack. ctrl+click it to see what's going on there.
            base.OnEnter();

            PlayAttackAnimation();
        }

        /// <summary>
        /// nani? what is this? overlapAttack? See <see cref="BaseMeleeAttack.overlapAttack"/> for more info.
        /// </summary>
        protected override void ModifyOverlapAttack(OverlapAttack overlapAttack)
        {
            base.ModifyOverlapAttack(overlapAttack);

            //third hit in the combo applies a dot
            if (isComboFinisher)
            {
                R2API.DamageAPI.AddModdedDamageType(overlapAttack, HenryContent.CharacterDamageTypes.comboFinisherDebuffDamage);
            }
        }

        private string GetComboMuzzle()
        {
            //spawn your swing effect at a different point based on your combo step.
            switch (swingIndex)
            {
                default:
                case 0:
                    return "SwingLeft";
                case 1:
                    return "SwingRight";
                case 2:
                    return "SwingCenter";
            }
        }

        protected override void PlayAttackAnimation()
        {
            //play a adifferent animation based on what step of the combo you are currently in.
            switch (swingIndex)
            {
                case 0:
                    PlayCrossfade("Gesture, Override", "Slash1", playbackRateParam, duration, 0.1f * duration);
                    break;
                case 1:
                    PlayCrossfade("Gesture, Override", "Slash2", playbackRateParam, duration, 0.1f * duration);
                    break;
                case 2:
                    PlayCrossfade("Gesture, Override", "SlashFinisher", playbackRateParam, duration, 0.1f * duration);
                    break;
            }
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

        //add these functions for steppedskilldefs
        //bit advanced so don't worry about this, it's for networking.
        //long story short this syncs a value from authority (current player) to all other clients, so the swingIndex is the same for all machines
        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write(swingIndex);
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            swingIndex = reader.ReadInt32();
        }
    }
}