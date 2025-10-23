using UnityEngine;

namespace Drydock
{
    /// <summary>
    /// Alternate keel for more/less stability
    /// </summary>
    public class KeelWeight : MonoBehaviour
    {
        public BoatKeel boatKeel;
        private Vector3 startCoM;
        public Vector3 localCoM = Vector3.zero;

        public void Awake()
        {
            if (boatKeel == null)
            {
                boatKeel = GetComponentInParent<BoatKeel>();
            }
            startCoM = boatKeel.centerOfMass;
        }
        public void OnEnable()
        {
            boatKeel.centerOfMass = localCoM;
        }
        public void OnDisable()
        {
            boatKeel.centerOfMass = startCoM;
        }
    }
}
