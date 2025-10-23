using UnityEngine;

namespace SE_Bridge
{
    public class MeshSwapper : MonoBehaviour
    {
        public Mesh mesh;
        public Mesh oldMesh;
        public MeshFilter target;
        public string targetPath; // path inside BoatModel/WalkCol
        public bool targetWalkCol;
        public Transform targetParent;

        private void Awake()
        {
            if (target == null && targetParent != null && !string.IsNullOrEmpty(targetPath))
            {
                target = targetParent.Find(targetPath)?.GetComponent<MeshFilter>();
            }
        }

        public void OnEnable()
        {
            if (target != null)
            {
                if (oldMesh == null)
                {
                    oldMesh = target.sharedMesh;
                }
                target.sharedMesh = mesh;
                if (targetWalkCol)
                {
                    target.GetComponent<MeshCollider>().sharedMesh = mesh;
                }
            }
        }
        public void OnDisable()
        {
            if (target != null && oldMesh != null)
            {
                target.sharedMesh = oldMesh;
                if (targetWalkCol)
                {
                    target.GetComponent<MeshCollider>().sharedMesh = oldMesh;
                }
            }
        }

    }
}
