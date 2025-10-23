using System.IO;
using System;
using OVRSimpleJSON;
using UnityEngine;
using System.Collections.Generic;

namespace Drydock
{
    public static class IDManager
    {
        public static Dictionary<string, int> IDMap;
        const string fileName = "ShipIDs.json";

        public static void WriteFile()
        {
            if (IDMap == null) Debug.LogError("Drydock: failed to write id file: Dictionary is null!!");
            string path = Directory.GetParent(Plugin.Instance.Info.Location).FullName + fileName;

            JSONArray arr = new JSONArray();
            foreach (var id in IDMap)
            {
                arr.Add(id.Key, id.Value);
            }
            File.WriteAllText(path, arr.ToString());
        }

        public static void ReadFile()
        {
            IDMap = new Dictionary<string, int>();
            string path = Directory.GetParent(Plugin.Instance.Info.Location).FullName + fileName;
            if (!File.Exists(path))
            {
                Debug.Log("Drydock: File not found!");
            }
            else
            {
                JSONArray json = JSON.Parse(File.ReadAllText(path)).AsArray;
                foreach (var item in json)
                {
                    IDMap.Add(item.Key, item.Value);
                }
            }
        }

        // creates a mapping of namespaced id to scene index.
        // This will be saved to a file to prevent issues over the life of a save file
        public static int MapID(StructureInfo boat)
        {
            // if it's in the map already, we can skip it
            if (IDMap.TryGetValue(boat.namespacedID, out int index))
            {
                return index;
            }
            var objects = SaveLoadManager.instance.GetCurrentObjects();
            var saveable = boat.GetComponent<SaveableObject>();
            var mooringRopes = boat.GetComponent<BoatMooringRopes>();
            int newID = saveable.sceneIndex;
            for (int i = saveable.sceneIndex; i < objects.Length; i++)
            {
                bool available = true;
                if (objects[i] != null) continue;

                if (mooringRopes != null) // skip if we don't have any ropes
                {
                    // starting at 1 to avoid extra clutter. +2 accounts for start plus padding
                    for (int j = 1; j < mooringRopes.ropes.Length + 2; j++)
                    {
                        if (i + j >= objects.Length)
                        {
                            Array.Resize(ref objects, j + 10);
                            available = true;
                            break;
                        }
                        if (objects[i + j] != null)
                        {
                            available = false;
                            break;
                        }
                    }
                }

                if (available == false) continue;

                newID = i;

            }
            saveable.sceneIndex = newID;
            IDMap.Add(boat.namespacedID, newID);

            return newID;
        }
    }
}
