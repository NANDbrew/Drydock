using UnityEngine;

namespace Drydock
{
    [ExecuteInEditMode]
    public class StructureInfo : MonoBehaviour
    {
        public string namespacedID;

        public Transform walkCol;

        public int parentIslandIndex;
        
        public Bounds walkColBounds;

        public virtual void OnValidate()
        {
            if (walkCol != null)
            {
                walkColBounds = GetMaxBounds(walkCol.gameObject);
            }
        }

        public Bounds GetMaxBounds(GameObject g)
        {
            var renderers = g.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0) return new Bounds(g.transform.position, Vector3.zero);
            var b = renderers[0].bounds;
            foreach (Renderer r in renderers)
            {
                b.Encapsulate(r.bounds);
            }
            return b;
        }
    }
}
