using RoR2;
using RoR2.Skills;
using System;
using UnityEngine;

namespace HenryAPI.Modules
{
    //consolidate contentaddition here in case something breaks and/or want to move to r2api
    public static class Content
    {
        public static void AddCharacterBodyPrefab(ContentPackContainer contentPack, GameObject bprefab)
        {
            contentPack.bodyPrefabs.Add(bprefab);
        }

        public static void AddMasterPrefab(ContentPackContainer contentPack, GameObject prefab)
        {
            contentPack.masterPrefabs.Add(prefab);
        }

        public static void AddProjectilePrefab(ContentPackContainer contentPack, GameObject prefab)
        {
            contentPack.projectilePrefabs.Add(prefab);
        }

        public static void AddSurvivorDef(ContentPackContainer contentPack, SurvivorDef survivorDef)
        {
            contentPack.survivorDefs.Add(survivorDef);
        }
        public static void CreateSurvivor(ContentPackContainer contentPack, GameObject bodyPrefab, GameObject displayPrefab, Color charColor, string tokenPrefix) { CreateSurvivor(contentPack, bodyPrefab, displayPrefab, charColor, tokenPrefix, null, 100f); }
        public static void CreateSurvivor(ContentPackContainer contentPack, GameObject bodyPrefab, GameObject displayPrefab, Color charColor, string tokenPrefix, float sortPosition) { CreateSurvivor(contentPack, bodyPrefab, displayPrefab, charColor, tokenPrefix, null, sortPosition); }
        public static void CreateSurvivor(ContentPackContainer contentPack, GameObject bodyPrefab, GameObject displayPrefab, Color charColor, string tokenPrefix, UnlockableDef unlockableDef) { CreateSurvivor(contentPack, bodyPrefab, displayPrefab, charColor, tokenPrefix, unlockableDef, 100f); }
        public static void CreateSurvivor(ContentPackContainer contentPack, GameObject bodyPrefab, GameObject displayPrefab, Color charColor, string tokenPrefix, UnlockableDef unlockableDef, float sortPosition)
        {
            SurvivorDef survivorDef = ScriptableObject.CreateInstance<SurvivorDef>();
            survivorDef.bodyPrefab = bodyPrefab;
            survivorDef.displayPrefab = displayPrefab;
            survivorDef.primaryColor = charColor;

            survivorDef.cachedName = bodyPrefab.name.Replace("Body", "");
            survivorDef.displayNameToken = tokenPrefix + "NAME";
            survivorDef.descriptionToken = tokenPrefix + "DESCRIPTION";
            survivorDef.outroFlavorToken = tokenPrefix + "OUTRO_FLAVOR";
            survivorDef.mainEndingEscapeFailureFlavorToken = tokenPrefix + "OUTRO_FAILURE";

            survivorDef.desiredSortPosition = sortPosition;
            survivorDef.unlockableDef = unlockableDef;

            Modules.Content.AddSurvivorDef(contentPack, survivorDef);
        }

        public static void AddUnlockableDef(ContentPackContainer contentPack, UnlockableDef unlockableDef)
        {
            contentPack.unlockableDefs.Add(unlockableDef);
        }
        public static UnlockableDef CreateAndAddUnlockbleDef(ContentPackContainer contentPack, string identifier, string nameToken, Sprite achievementIcon)
        {
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.cachedName = identifier;
            unlockableDef.nameToken = nameToken;
            unlockableDef.achievementIcon = achievementIcon;

            AddUnlockableDef(contentPack, unlockableDef);

            return unlockableDef;
        }

        public static void AddSkillDef(ContentPackContainer contentPack, SkillDef skillDef)
        {
            contentPack.skillDefs.Add(skillDef);
        }

        public static void AddSkillFamily(ContentPackContainer contentPack, SkillFamily skillFamily)
        {
            contentPack.skillFamilies.Add(skillFamily);
        }

        public static void AddEntityState(ContentPackContainer contentPack, Type entityState)
        {
            contentPack.entityStates.Add(entityState);
        }

        public static void AddBuffDef(ContentPackContainer contentPack, BuffDef buffDef)
        {
            contentPack.buffDefs.Add(buffDef);
        }
        public static BuffDef CreateAndAddBuff(ContentPackContainer contentPack, string buffName, Sprite buffIcon, Color buffColor, bool canStack, bool isDebuff)
        {
            BuffDef buffDef = ScriptableObject.CreateInstance<BuffDef>();
            buffDef.name = buffName;
            buffDef.buffColor = buffColor;
            buffDef.canStack = canStack;
            buffDef.isDebuff = isDebuff;
            buffDef.eliteDef = null;
            buffDef.iconSprite = buffIcon;

            AddBuffDef(contentPack, buffDef);

            return buffDef;
        }

        public static void AddEffectDef(ContentPackContainer contentPack, EffectDef effectDef)
        {
            contentPack.effectDefs.Add(effectDef);
        }
        public static EffectDef CreateAndAddEffectDef(ContentPackContainer contentPack, GameObject effectPrefab)
        {
            EffectDef effectDef = new EffectDef(effectPrefab);

            AddEffectDef(contentPack, effectDef);

            return effectDef;
        }

        public static void AddNetworkSoundEventDef(ContentPackContainer contentPack, NetworkSoundEventDef networkSoundEventDef)
        {
            contentPack.networkSoundEventDefs.Add(networkSoundEventDef);
        }
        public static NetworkSoundEventDef CreateAndAddNetworkSoundEventDef(ContentPackContainer contentPack, string eventName)
        {
            NetworkSoundEventDef networkSoundEventDef = ScriptableObject.CreateInstance<NetworkSoundEventDef>();
            networkSoundEventDef.akId = AkSoundEngine.GetIDFromString(eventName);
            networkSoundEventDef.eventName = eventName;

            AddNetworkSoundEventDef(contentPack, networkSoundEventDef);
            
            return networkSoundEventDef;
        }
    }
}