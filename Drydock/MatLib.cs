using UnityEngine;
using Crest;

namespace Drydock
{
    /// <summary>
    /// Manages a material library to easily access and assign materials that are not correctly decompiled (e.g Crest materials)
    /// </summary>
    public class MatLib
    {   
        public static Material hullMask;
        public static Material waterFoam;
        public static Material objectInteraction;
        public static Material damageWater;
        public static Material splashMask;
        public static Material overflow;
        //public static Material flags;

        public static void RegisterMaterials()
        {   //save the materials from the cog to the variables so that they can be assigned to loaded boats
            GameObject cog = SaveLoadManager.instance.GetCurrentObjects()[40].gameObject;
            Transform mediSmall = cog.transform.Find("medi small");
            hullMask = mediSmall.Find("mask").GetComponent<MeshRenderer>().sharedMaterial;
            waterFoam = cog.GetComponent<FoamAdjuster>().foam.GetComponent<MeshRenderer>().material;//cog.transform.Find("WaterFoam").GetComponent<MeshRenderer>().sharedMaterial;
            objectInteraction = cog.GetComponentInChildren<SphereWaterInteraction>().GetComponent<MeshRenderer>().sharedMaterial;
            damageWater = mediSmall.Find("damage_water").GetComponent<MeshRenderer>().sharedMaterial;
            splashMask = mediSmall.Find("mask_splash").GetComponent<MeshRenderer>().sharedMaterial;
            overflow = cog.transform.Find("overflow particles").GetComponent<Renderer>().sharedMaterial;
        }
    }
}
