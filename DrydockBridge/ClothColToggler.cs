using UnityEngine;

namespace Drydock
{
    /// <summary>
    /// used to enable/disable colliders in Mast.mastCols when changing options (shrouds, for example)
    /// without this, sails will collide with all Capsule Colliders in Mast.mastCols, even disabled ones
    /// </summary>
    public class ClothColToggler : MonoBehaviour
    {
        float[] radii;
        public CapsuleCollider[] cols;

        public void Awake()
        {
            if (cols == null) cols = GetComponentsInChildren<CapsuleCollider>();
            radii = new float[cols.Length];
            for (int i = 0; i < cols.Length; i++)
            {
                radii[i] = cols[i].radius;
            }
            //radius = col.radius;
        }
        public void OnEnable()
        {
            for (int i = 0;i < cols.Length; i++)
            {
                cols[i].radius = radii[i];
            }
            //col.radius = radius;
        }
        public void OnDisable()
        {
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i].radius = 0;
            }
            //col.radius = 0;
        }
    }
}
