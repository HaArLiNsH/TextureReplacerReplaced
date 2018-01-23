using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TextureReplacerReplaced
{
    public class AddCloth : MonoBehaviour
    {

        public List<GameObject> ClothItems = null;          // cloth list to add


        private GameObject[] cpClothItem = null;                // copy of the clothes
        private SkinnedMeshRenderer[] skClothItem = null;   // copy of the skin fo the clothes
        private Transform[] skBone = null;                  //  bones copia auxiliar para añadir a lista


        void Start()
        {
            if (ClothItems != null)
            {       // do the thing
                AddItemCloth(ClothItems.Count);
            }
        }

        private void AddItemCloth(int count)
        {

            cpClothItem = new GameObject[count]; // preparar lista objetos
            skClothItem = new SkinnedMeshRenderer[count];    // prepara lista skinned

            // añadir los items al personaje
            // prepara copia lista esqueletos
            for (int ci = 0; ci < count; ci++)
            {
                // cargar esqueleto prenda
                SkinnedMeshRenderer[] skCloth = ClothItems[ci].GetComponentsInChildren<SkinnedMeshRenderer>();
                foreach (SkinnedMeshRenderer SMR in skCloth)
                {
                    // duplicar objeto
                    cpClothItem[ci] = new GameObject(SMR.gameObject.name);
                    // emparentar con personaje
                    cpClothItem[ci].transform.parent = transform;
                    skClothItem[ci] = cpClothItem[ci].AddComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
                    // hacer copia del esqueleto
                    skBone = new Transform[SMR.bones.Length];
                    for (int ii = 0; ii < SMR.bones.Length; ii++)
                        skBone[ii] = FindChildByName(SMR.bones[ii].name, transform);
                    // Nuevo objeto reemplaza al modelo original, pero con esqueleto
                    // reasignado + animacion + materiales. Este objeto no debe eliminarse.
                    skClothItem[ci].bones = skBone;
                    skClothItem[ci].sharedMesh = SMR.sharedMesh;
                    skClothItem[ci].materials = SMR.materials;
                    // Liberar memoria de objetos en deshuso
                    UnityEngine.Object.Destroy(SMR);
                    for (int ii = 0; ii < skCloth.Length; ii++)
                        UnityEngine.Object.Destroy(skCloth[ii]);
                }
                for (int ii = 0; ii < count; ii++)
                    Destroy(ClothItems[ii]);
                skBone = null;
            }
        }


        private Transform FindChildByName(string ThisName, Transform ThisGObj)
        {
            Transform ReturnObj;

            if (ThisGObj.name == ThisName)
                return ThisGObj.transform;
            foreach (Transform child in ThisGObj)
            {
                ReturnObj = FindChildByName(ThisName, child);
                if (ReturnObj != null) return ReturnObj;
            }
            return null;
        }

    }
}


// Basado en proyecto de 'masterprompt' <Joined:Jan 6, 2009>
// -http://forum.unity3d.com/threads/stitch-multiple-body-parts-into-one-character.16485/
// Modificado para uso de multiples items animados en personaje
// con eliminacion de memoria de recursos que no son necesarios




