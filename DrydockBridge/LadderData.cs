using UnityEngine;

namespace Drydock
{
    /// <summary>
    /// Data required for AnimatedLadder, which will be added automatically at runtime
    /// </summary>
    public class LadderData : MonoBehaviour
    {
        public Transform walkCol;
        public Transform[] targets;
    }
}
