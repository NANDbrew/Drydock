using HarmonyLib;

namespace Drydock
{
    [HarmonyPatch(typeof(PlayerEmbarkDisembarkTrigger), "Update")]
    internal static class PlayerEmbarkPatch
    {
        public static bool Prefix(bool __runOriginal)
        {
            // don't allow player embarking/disembarking while ladder motion is happening
            if (AnimatedLadder.animating || !__runOriginal) return false;
            return true;

        }

    }
}
