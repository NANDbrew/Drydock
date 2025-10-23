using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Drydock
{
    public static class AssetTools
    {
        public static List<AssetBundle> bundles;
        public static AssetBundle bundle2;
        static string basePath = "";
        const string assetDir = "Ships";
        const string libFile = "DrydockBridge.dll";

        private static float walkColOffset;

        public static void LoadAssetBundles()
        {
            basePath = Directory.GetParent(Plugin.Instance.Info.Location).FullName;
            try
            {
                Assembly.LoadFrom(Path.Combine(basePath, libFile));
                Debug.Log("ShipyardExpansion: SE bridge loaded successfully");
            } 
            catch { Debug.LogError("Drydock: failed to load other assembly!"); }

            bundles = new List<AssetBundle>();
            foreach (var file in Directory.GetFiles(assetDir, "*.ship"))
            { 
                bundles.Add(AssetBundle.LoadFromFile(file));
            }


        }

        public static void LoadBoats()
        {
            foreach (var bundle in bundles)
            {
                foreach (GameObject prefab in (GameObject[])bundle.LoadAllAssets())
                {
                    StructureInfo structure = prefab.GetComponent<StructureInfo>();
                    if (structure is BoatInfo info)
                    {
                        PreparePrefab(prefab);
                        InstantiatePrefab(info);
                        // shift the next walk col position so it doesn't collide
                        walkColOffset += info.walkColBounds.size.z;
                    }
                    else
                    {
                        InstantiatePrefab(structure);
                    }
                }
            }
        }

        

        public static void PreparePrefab(GameObject prefab)
        {
            #if DEBUG
                Debug.Log("working on ladders and materials");
            #endif

            var data = prefab.GetComponent<StructureInfo>();

            if (data.namespacedID.Length > 0)
            {
                IDManager.MapID(data);
            }
            if (data is BoatInfo boatData)
            {
                // add components that can't be in the bridge lib
                for (int i = 0; i < boatData.ladders.Length; i++)
                {
                    boatData.ladders[i].gameObject.AddComponent<AnimatedLadder>();
                }

                // fix missing materials
                for (int i = 0; i < boatData.waterInteractionSpheres.Length; i++)
                {
                    boatData.waterInteractionSpheres[i].sharedMaterial = MatLib.objectInteraction;
                }

                boatData.waterFoam.sharedMaterial = MatLib.waterFoam;

                boatData.damageWater.sharedMaterial = MatLib.damageWater;

                boatData.splashMask.sharedMaterial = MatLib.splashMask;

                boatData.hullMask.sharedMaterial = MatLib.hullMask;

                boatData.overflowParticles.sharedMaterial = MatLib.overflow;
            }
        }

        public static void InstantiatePrefab(StructureInfo prefab)
        {
            GameObject boat;
            if (prefab.parentIslandIndex > -1 && prefab.parentIslandIndex < Refs.islands.Length && Refs.islands[prefab.parentIslandIndex] != null)
            {
                boat = GameObject.Instantiate(prefab.gameObject, Refs.islands[prefab.parentIslandIndex], false);
            }
            else
            {
                boat = GameObject.Instantiate(prefab.gameObject, FloatingOriginManager.instance.RealPosToShiftingPos(prefab.transform.position), prefab.transform.rotation, Refs.shiftingWorld.transform);
            }

            if (boat.GetComponent<StructureInfo>() is StructureInfo info && info.walkCol != null)
            {
                info.walkCol.parent = Plugin.walkColContainer;
                // position walk col with clearance based on precomputed bounds
                info.walkCol.localPosition = new Vector3(-200, 0, -200 + walkColOffset + info.walkColBounds.size.z);
            }
        }

    }
}

