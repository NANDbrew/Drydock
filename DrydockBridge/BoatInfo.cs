using Crest;
using SE_Bridge;
using System;
using UnityEngine;

namespace Drydock
{
    /// <summary>
    /// References for Drydock runtime setup
    /// </summary>
    [ExecuteInEditMode]
    public class BoatInfo : StructureInfo
    {

        public LadderData[] ladders;
        
        public MeshRenderer[] waterInteractionSpheres;

        public MeshRenderer waterFoam;

        public MeshRenderer damageWater;

        public MeshRenderer splashMask;

        public MeshRenderer hullMask;

        public Renderer[] overflowParticles;


        public bool validate = false; // editor only. is a stand-in for a button
        public override void OnValidate()
        {
            base.OnValidate();

            if (validate)
            {
                ladders = GetComponentsInChildren<LadderData>();
                if (waterInteractionSpheres == null)
                {
                    var spheres = GetComponentsInChildren<SphereWaterInteraction>();
                    waterInteractionSpheres = new MeshRenderer[spheres.Length];
                    for (int i = 0; i < spheres.Length; i++)
                    {
                        waterInteractionSpheres[i] = spheres[i].GetComponent<MeshRenderer>();
                    }
                }
                if (waterFoam == null)
                {
                    waterFoam = GetComponentInChildren<RegisterFoamInput>().GetComponent<MeshRenderer>();
                }
                if (damageWater == null)
                {
                    damageWater = GetComponentInChildren<BoatDamageWater>().GetComponent<MeshRenderer>();
                }
                if (hullMask == null)
                {
                    hullMask = GetComponentInChildren<RegisterClipSurfaceInput>().GetComponent<MeshRenderer>();
                }
                if (overflowParticles == null)
                {
                    var list = GetComponentsInChildren<WaveSplashZone>();
                    overflowParticles = new Renderer[list.Length];
                    for (int i = 0; i < list.Length; i++)
                    {
                        overflowParticles[i] = list[i].GetComponent<Renderer>();
                    }
                }
                validate = false;
            }


        }
    }


}
