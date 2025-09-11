using RoR2;
using RoR2.ContentManagement;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HenryAPI.Modules {
    public class ContentPackContainer : IContentPackProvider
    {
        internal ContentPack contentPack = new ContentPack();
        public string identifier => HenryAPIPlugin.MODUID;

        public List<GameObject> bodyPrefabs = new List<GameObject>();
        public List<GameObject> masterPrefabs = new List<GameObject>();
        public List<GameObject> projectilePrefabs = new List<GameObject>();

        public  List<SurvivorDef> survivorDefs = new List<SurvivorDef>();
        public  List<UnlockableDef> unlockableDefs = new List<UnlockableDef>();

        public List<SkillFamily> skillFamilies = new List<SkillFamily>();
        public List<SkillDef> skillDefs = new List<SkillDef>();
        public List<Type> entityStates = new List<Type>();

        public List<BuffDef> buffDefs = new List<BuffDef>();
        public List<EffectDef> effectDefs = new List<EffectDef>();

        public List<NetworkSoundEventDef> networkSoundEventDefs = new List<NetworkSoundEventDef>();

        public void Initialize()
        {
            ContentManager.collectContentPackProviders += ContentManager_collectContentPackProviders;
        }

        private void ContentManager_collectContentPackProviders(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
        {
            addContentPackProvider(this);
        }

        public System.Collections.IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
        {
            this.contentPack.identifier = this.identifier;

            contentPack.bodyPrefabs.Add(bodyPrefabs.ToArray());
            contentPack.masterPrefabs.Add(masterPrefabs.ToArray());
            contentPack.projectilePrefabs.Add(projectilePrefabs.ToArray());

            contentPack.survivorDefs.Add(survivorDefs.ToArray());
            contentPack.unlockableDefs.Add(unlockableDefs.ToArray());

            contentPack.skillDefs.Add(skillDefs.ToArray());
            contentPack.skillFamilies.Add(skillFamilies.ToArray());
            contentPack.entityStateTypes.Add(entityStates.ToArray());

            contentPack.buffDefs.Add(buffDefs.ToArray());
            contentPack.effectDefs.Add(effectDefs.ToArray());

            contentPack.networkSoundEventDefs.Add(networkSoundEventDefs.ToArray());

            args.ReportProgress(1f);
            yield break;
        }

        public System.Collections.IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
        {
            ContentPack.Copy(this.contentPack, args.output);
            args.ReportProgress(1f);
            yield break;
        }

        public System.Collections.IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
        {
            args.ReportProgress(1f);
            yield break;
        }
    }
}