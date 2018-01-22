//Remove "Collar" from body of Kerbal mesh, moves it into its own mesh parented to the helmet
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[KSPAddon(KSPAddon.Startup.MainMenu, true)]
class ModifyKerbalHelmetModel : MonoBehaviour
{
    private static readonly Color CollarMaskColor = Color.red;

    private void Start()
    {
        var suitMask = new Texture2D(1, 1);

        // easiest way to identify the collar is just to take any suit texture, paint the relevant areas
        // whatever the collar mask color is, and sample it when checking vertices to identify the ones
        // associated with the collar. Only needs to be done at startup, texture can be destroyed as soon
        // as we're done
        if (!suitMask.LoadImage(File.ReadAllBytes(Application.dataPath + "/../GameData/suit_mask.png")))
        {
            Destroy(suitMask);
            Debug.LogError("missing suit mask");
            return;
        }

        var kerbals = new[]
        {
            PartLoader.getPartInfoByName("kerbalEVA"),
            PartLoader.getPartInfoByName("kerbalEVAfemale")
        };

        foreach (var k in kerbals)
        {
            if (k == null)
            {
                Debug.LogError("Failed to retrieve a kerbalEVA part prefab");
                continue;
            }

            var keva = k.partPrefab.GetComponent<KerbalEVA>();

            if (keva == null)
            {
                Debug.LogError("Missing KerbalEVA script on " + k.name);
                continue;
            }

            var bodyModel = keva.transform.Find("model01/female01/body01") ?? keva.transform.Find("model01/body01");

            if (bodyModel == null)
            {
                Debug.LogWarning("KerbalEVA transform hierarchy unexpected: " + k.name);
                continue;
            }

            var bodySmr = bodyModel.GetComponent<SkinnedMeshRenderer>();

            if (bodySmr == null)
            {
                Debug.LogWarning("KerbalEVA does not have SMR in expected place: " + k.name);
                continue;
            }

            try
            {
                MergeCollarToHelmet(keva, suitMask);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                Debug.LogError("Failed to edit suit collar: " + k.name);
            }
        }

        Destroy(suitMask);
        Destroy(gameObject);
    }


    private static void MergeCollarToHelmet([NotNull] KerbalEVA keva, [NotNull] Texture2D collarSuitMask)
    {
        if (keva == null) throw new ArgumentNullException("keva");
        if (collarSuitMask == null) throw new ArgumentNullException("collarSuitMask");

        var body = keva.transform.Find("model01/female01/body01") ?? keva.transform.Find("model01/body01");

        if (body == null) throw new ArgumentException("couldn't find body transform");

        var helmetModel = keva.transform.Find("model01/helmet01") ?? keva.transform.Find("model01/female01/helmet01");

        if (helmetModel == null)
        {

            throw new ArgumentException("couldn't find helmet transform");
        }

        var kerbalBody = body.GetComponent<SkinnedMeshRenderer>();

        if (kerbalBody == null)
            throw new ArgumentException("couldn't find SkinnedMeshRenderer for body");

        var collarMesh = new Mesh();

        var srcVertices = kerbalBody.sharedMesh.vertices;
        var srcUvs = kerbalBody.sharedMesh.uv;
        var srcTriangles = kerbalBody.sharedMesh.triangles;
        var srcBoneWeights = kerbalBody.sharedMesh.boneWeights;
        var srcNormals = kerbalBody.sharedMesh.normals;

        var vertices = new List<Vector3>();
        var uvs = new List<Vector2>();
        var triangles = new List<int>();
        var normals = new List<Vector3>();
        var boneWeights = new List<BoneWeight>();


        var srcVertexToDestVertexMap = new Dictionary<int, int>(); // [src index, dest index]

        // collect every vertex that's a part of the collar of the suit
        for (int i = 0; i < srcVertices.Length; ++i)
        {
            var maskColor = collarSuitMask.GetPixel((int)(srcUvs[i].x * collarSuitMask.width),
                (int)(srcUvs[i].y * collarSuitMask.height));

            if (!Mathf.Approximately(CollarMaskColor.r, maskColor.r) ||
                !Mathf.Approximately(CollarMaskColor.g, maskColor.g) ||
                !Mathf.Approximately(CollarMaskColor.b, maskColor.b)) continue;

            // this vertex is part of the collar, we just need its data so nothing
            // fancy going on here
            vertices.Add(srcVertices[i]);
            uvs.Add(srcUvs[i]);
            normals.Add(srcNormals[i]);
            boneWeights.Add(srcBoneWeights[i]);

            srcVertexToDestVertexMap[i] = vertices.Count - 1; // we need to know how old vertex index relates to new vertex index so
                                                              // we can figure out how to map triangles
        }


        // collect every triangle that's a part of the suit collar. Use the mapping of 
        // old vertex index -> new vertex index to figure out correct indices
        for (int tri = 0; tri < srcTriangles.Length; tri += 3)
        {
            // if every vertex is a part of the collar, tri belongs to the collar
            if (srcVertexToDestVertexMap.ContainsKey(srcTriangles[tri + 0]) &&
                srcVertexToDestVertexMap.ContainsKey(srcTriangles[tri + 1]) &&
                srcVertexToDestVertexMap.ContainsKey(srcTriangles[tri + 2]))
            {
                // 1. Add this tri to the collar
                triangles.Add(srcVertexToDestVertexMap[srcTriangles[tri + 0]]);
                triangles.Add(srcVertexToDestVertexMap[srcTriangles[tri + 1]]);
                triangles.Add(srcVertexToDestVertexMap[srcTriangles[tri + 2]]);

                // 2. remove this tri from the suit. Leaves vertices intact
                if (tri < srcTriangles.Length - 3) // if last tri, no need to shift elements
                {
                    Array.Copy(srcTriangles, tri + 3, srcTriangles, tri, srcTriangles.Length - (tri + 3));
                }

                Array.Resize(ref srcTriangles, srcTriangles.Length - 3);
                tri -= 3;
            }
        }

        // sanity check, because silently failing is bad 
        if (vertices.Count == 0 || triangles.Count == 0)
        {
            Debug.LogWarning("Suit mask seems wrong -- no vertices or triangles matched");
        }

        // create collar mesh
        collarMesh.vertices = vertices.ToArray();
        collarMesh.triangles = triangles.ToArray();
        collarMesh.uv = uvs.ToArray();
        collarMesh.normals = normals.ToArray();
        collarMesh.bindposes = kerbalBody.sharedMesh.bindposes;
        collarMesh.boneWeights = boneWeights.ToArray();
        collarMesh.Optimize();

        // update suit mesh's triangles (we removed some)
        kerbalBody.sharedMesh.triangles = srcTriangles;
        kerbalBody.sharedMesh.Optimize();

        // and here we set up the transform + renderer for the collar mesh just made
        var clone = new GameObject("Collar.Helmet");

        clone.AddComponent<MeshFilter>().sharedMesh = collarMesh;

        var collarRenderer = clone.AddComponent<SkinnedMeshRenderer>();

        collarRenderer.sharedMaterial = kerbalBody.sharedMaterial;
        collarRenderer.sharedMesh = collarMesh;
        collarRenderer.bones = kerbalBody.bones;

        clone.transform.position = kerbalBody.transform.position;
        clone.transform.rotation = kerbalBody.transform.rotation;
        clone.layer = kerbalBody.gameObject.layer;
        clone.transform.parent = helmetModel.transform; // now disabling the helmet will disable the helmet's collar, too
    }
}