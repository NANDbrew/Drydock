using System.Collections;
using System.Linq;
using UnityEngine;

namespace Drydock
{
    /// <summary>
    /// Interpolates the player's position to the farthest target (when activated)
    /// This only works if the player is already on the same boat
    /// </summary>

    public class AnimatedLadder : GoPointerButton
    {
        public static bool animating;
        public Transform[] targets;
        CharacterController player;
        public Transform walkCol;

        public override void Start()
        {
            base.Start();
            player = Refs.charController;

            if (GetComponent<LadderData>() is LadderData data)
            {
                targets = data.targets;
                walkCol = data.walkCol;
            }
            if (walkCol == null)
            {
                walkCol = GetComponentInParent<BoatRefs>().walkCol;
            }
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].SetParent(walkCol, false);
                targets[i].gameObject.layer = 8;
            }
        }
        public override void OnActivate()
        {
            if (player.transform.parent == walkCol && targets.Length >= 1)
            {
                Transform target = targets.OrderBy(t => (t.transform.localPosition - player.transform.localPosition).sqrMagnitude).Last();
                this.StartCoroutine(LerpPlayerPos(target));
            }
            else Debug.Log("player is not on the same boat");
        }
        private IEnumerator LerpPlayerPos(Transform target)
        {
            animating = true;
            player.enabled = false;
            Vector3 start = player.transform.localPosition;
            float lerpTime = Vector3.Distance(start, target.localPosition) * 0.1f;
            if (start.y < target.localPosition.y || Plugin.climbSpeed.Value > 10) { lerpTime /= Plugin.climbSpeed.Value * 0.1f; }
            for (float t = 0f; t < 1f; t += Time.deltaTime / lerpTime)
            {
                player.transform.localPosition = Vector3.Lerp(start, target.localPosition, t);
                yield return new WaitForEndOfFrame();
                if (GameInput.GetKey(InputName.Activate)) { break; }
            }
            player.enabled = true;
            animating = false;
        }
    }
}
