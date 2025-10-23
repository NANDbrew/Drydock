using System.Reflection;
using UnityEngine;

namespace Drydock
{
    public class ObjectToggler : MonoBehaviour
    {
        public GameObject[] offObjects = new GameObject[0];
        public GameObject[] onObjects = new GameObject[0];

        public void OnEnable()
        {
            ToggleArray(offObjects, false);
            ToggleArray(onObjects, true);

        }
        public void OnDisable()
        {
            ToggleArray(offObjects, true);
            ToggleArray(onObjects, false);
        }

        public static void ToggleArray(GameObject[] parts, bool state)
        {
            foreach (GameObject obj in parts)
            {
                if (obj.GetComponent<GPButtonTrapdoor>() is GPButtonTrapdoor door)
                {
                    Transform col = (Transform)typeof(GPButtonTrapdoor).GetField("walkCol", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(door);
                    if (col) col.gameObject.SetActive(state);
                }
                obj.SetActive(state);
            }
        }
    }
}
