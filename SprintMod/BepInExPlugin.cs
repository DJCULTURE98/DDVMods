﻿using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System.Reflection;

namespace SprintMod
{
    [BepInPlugin("aedenthorn.SprintMod", "SprintMod", "0.1.0")]
    public class BepInExPlugin : BasePlugin
    {
        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<int> nexusID;
        public static ConfigEntry<float> multiplier;

        public static BepInExPlugin context;
        public static void Dbgl(object str)
        {
            if (isDebug.Value && str != null)
                context.Log.LogInfo(str);
        }

        public override void Load()
        {
            context = this;
            modEnabled = Config.Bind("General", "Enabled", true, "Enable this mod");
            isDebug = Config.Bind<bool>("General", "IsDebug", true, "Enable debug logs");
            multiplier = Config.Bind<float>("General", "Multiplier", 1.5f, "Multiplier");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            AddComponent<MyUpdater>();
            Dbgl("mod loaded");
        }
    }
}
