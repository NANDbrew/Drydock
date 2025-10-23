using UnityEngine;
using System.Collections.Generic;
namespace SE_Bridge
{
    [ExecuteInEditMode]
    public class BoatCustomData : MonoBehaviour
    {
        public Mesh embarkColMesh;
        public Mast[] masts;
        public WindClothSimple[] flags;
        public LadderData[] ladders;
        public MeshSwapper[] meshSwappers;
        public GPButtonSteeringWheel[] tillers;
        public Transform walkCol;

        private void OnValidate()
        {
            masts = GetComponentsInChildren<Mast>();
            flags = GetComponentsInChildren<WindClothSimple>();
            doors = GetComponentsInChildren<GPButtonTrapdoor>();
            meshSwappers = GetComponentsInChildren<MeshSwapper>();
            tillers = GetComponentsInChildren<GPButtonSteeringWheel>();
        }
    }
}
