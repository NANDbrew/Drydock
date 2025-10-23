using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace Drydock
{
    [BepInPlugin(PLUGIN_ID, PLUGIN_NAME, PLUGIN_VERSION)]
    //[BepInDependency("com.app24.sailwindmoddinghelper", "2.0.3")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PLUGIN_ID = "com.nandbrew.drydock";
        public const string PLUGIN_NAME = "Drydock";
        public const string PLUGIN_VERSION = "0.0.1";

        public static Plugin Instance { get; private set; }
        //--settings--
        internal static ConfigEntry<int> climbSpeed;

        public static Transform walkColContainer;

        private void Awake()
        {
            Instance = this;
            shipIDs = new Map<string, int>();

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PLUGIN_ID);

            climbSpeed = Config.Bind("Settings", "Climb speed", 10, new ConfigDescription("Speed when climbing ladders", new AcceptableValueRange<int>(2, 15)));
        
            walkColContainer = GameObject.Find("walk cols").transform;

            AssetTools.LoadAssetBundles();
        }
    }
}
