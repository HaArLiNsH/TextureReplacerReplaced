using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TextureReplacerReplaced
{

    public class Stitcher
    {
        /// <summary>
        /// Stitch clothing onto an avatar.  Both clothing and avatar must be instantiated however clothing may be destroyed after.
        /// </summary>
        /// <param name="sourceClothing"> The source "cloth" we want to add to the target</param>
        /// <param name="targetAvatar">The target  that receive the "cloth", aka our kerbal</param>
        /// <returns>Newly created clothing on avatar</returns>
        public GameObject Stitch(GameObject sourceClothing, GameObject targetAvatar)
        {
            // we make a catalog of all the transforms of the targetAvatar . This is used to transfer the bones
            var boneCatalog = new TransformCatalog(targetAvatar.transform);

            Util.log("@@@@@@@@@@@@@@@@@@@@@@@@      BONECATALOG : @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

            foreach (string s in boneCatalog.Keys)
            {
                
                Util.log(s);
            }

            // we get all the SkinnedMeshRenderer of the source
            var sourceSMR = sourceClothing.GetComponentsInChildren<SkinnedMeshRenderer>();

            var kerbalSMR = targetAvatar.GetComponentsInChildren<SkinnedMeshRenderer>();

            SkinnedMeshRenderer baseBodySMR = new SkinnedMeshRenderer();

            foreach (var kerbalrenderer in kerbalSMR)
            {
                switch (kerbalrenderer.name)
                {
                    case "body01":
                    case "mesh_female_kerbalAstronaut01_body01":
                        baseBodySMR = kerbalrenderer;
                        break;
                }
            }

            // the new (empty) GameObject that will be our cloth choosing the targetAvatar as parent
            var targetClothing = AddChild(sourceClothing, targetAvatar.transform);

            // here is the magic.             
            foreach (var sourceRenderer in sourceSMR)
            {
                // we create the SkinnedMeshRenderer and copy the sharedMesh and material of the source
                var targetRenderer = AddSkinnedMeshRenderer(sourceRenderer, targetClothing);

                // we look, for each bone name we can find in the source, if we find it in the reference catalog. 
                // if so, we will copy the transform.bone from the catalog to the target.
                targetRenderer.bones = TranslateTransforms(baseBodySMR.bones, boneCatalog);
            }
            return targetClothing;
        }       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">the source we want to allocate a parent</param>
        /// <param name="parent">The target to be the .parent</param>
        /// <returns></returns>
        private GameObject AddChild(GameObject source, Transform parent)
        {
            source.transform.parent = parent;

            // cleaning
            foreach (Transform child in source.transform)
            {
                UnityEngine.Object.Destroy(child.gameObject);
            }

            return source;
        }

        /// <summary>
        /// we create new GameObjects from the skinnedMeshRenderer we saved before.
        /// we copy the materials and sharedMesh also.
        /// </summary>
        /// <param name="source">the SkinnedMeshRenderer we will add</param>
        /// <param name="parent">the GameObject used as target to be the parent</param>
        /// <returns></returns>
        private SkinnedMeshRenderer AddSkinnedMeshRenderer(SkinnedMeshRenderer source, GameObject parent)
        {
            // new object with the same name as the source
            GameObject meshObject = new GameObject(source.name);

            // we set the target as parent
            meshObject.transform.parent = parent.transform;

            // we create an empty SkinnedMeshRenderer and add it the newly created GameObject 
            var target = meshObject.AddComponent<SkinnedMeshRenderer>();

            // we copy the sharedMesh and the material of the source
            target.sharedMesh = source.sharedMesh;
            target.materials = source.materials;

            return target;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sources">the bones of the sources we want to tranfer</param>
        /// <param name="transformCatalog">the catalog of Transformations we will use as reference to make the transfer. it was made from the targetAvatar</param>
        /// <returns></returns>
        private Transform[] TranslateTransforms(Transform[] sources, TransformCatalog transformCatalog)
        {
            Util.log("?????????????? TranslateTransforms ????????????????");
            // we create a new bone list as big as the source SkinnedMeshRenderer
            var targets = new Transform[sources.Length];

            // we look, for each bone name we can find in the source, if we find it in the reference catalog. 
            // if so, we will copy the transform.bone from the catalog to the target.
            for (var index = 0; index < sources.Length; index++)
            {
                targets[index] = DictionaryExtensions.Find(transformCatalog, sources[index].name);
                
            }
                

            return targets;
        }

        /// <summary>
        /// a catalog made by collecting all the Transforms and their child
        /// This is a Dictionary composed of the transform.names and their transforms.
        /// </summary>
        #region TransformCatalog
        public class TransformCatalog : Dictionary<string, Transform>
        {
            #region Constructors
            public TransformCatalog(Transform transform)
            {
                Catalog(transform);
            }
            #endregion

            #region Catalog
            private void Catalog(Transform transform)
            {
                if (ContainsKey(transform.name))
                {
                    Remove(transform.name);
                    Add(transform.name, transform);
                }
                else
                    Add(transform.name, transform);
                foreach (Transform child in transform)
                    Catalog(child);
            }
            #endregion
        }
        #endregion

        

        #region DictionaryExtensions
        private class DictionaryExtensions
        {
            public static TValue Find<TKey, TValue>(Dictionary<TKey, TValue> source, TKey key)
            {
                TValue value;
                source.TryGetValue(key, out value);
                                
                return value;
            }
        }
        #endregion

    }
}