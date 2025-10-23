using UnityEngine;

namespace Drydock
{
    /// <summary>
    /// Watertight sideboards that change the boat's seaworthiness when enabled/disabled
    /// </summary>
    internal class Sideboards : MonoBehaviour
    {
        public BoatDamage damage;
        public WaveSplashZone[] splashZones;
        public float[] splashOffsets;
        public float offset;

        public void Awake()
        {
            if (damage == null)
            {
                damage = GetComponentInParent<BoatDamage>();
            }
            if (splashZones == null)
            {
                splashZones = damage.gameObject.GetComponentsInChildren<WaveSplashZone>();
            }
            splashOffsets = new float[splashZones.Length];
            for (int i = 0; i < splashZones.Length; i++)
            {
                splashOffsets[i] = splashZones[i].verticalOffset;
            }
        }
        public void OnEnable()
        {
            for (int i = 0; i < splashZones.Length; i++)
            {
                splashZones[i].verticalOffset = splashZones[i].verticalOffset + offset;
            }
        }
        public void OnDisable()
        {
            for (int i = 0; i < splashZones.Length; i++)
            {
                splashZones[i].verticalOffset = splashOffsets[i];
            }
        }
    }
}
