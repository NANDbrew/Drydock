using System.Reflection;
using UnityEngine;

namespace Drydock
{
    [RequireComponent(typeof(GPButtonTrapdoor))]
    public class DoorColToggler : MonoBehaviour
    {
        private GPButtonTrapdoor door;

        private void OnEnable()
        {
            ToggleDoorCol(true);
        }
        private void OnDisable()
        {
            ToggleDoorCol(false);
        }
        private void ToggleDoorCol(bool state)
        {
            if (door == null) door = GetComponent<GPButtonTrapdoor>();
            Transform col = (Transform)typeof(GPButtonTrapdoor).GetField("walkCol", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(door);
            col?.gameObject.SetActive(state);
        }
    }
}
