using BoneLib;
using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;
using BoneLib.Nullables;
using Il2CppSystem.Collections.Generic;
using iSurvivedBonelab.MonoBehaviours;
using MelonLoader;
using SLZ.Bonelab;
using SLZ.Interaction;
using SLZ.Marrow.Data;
using SLZ.Marrow.Pool;
using SLZ.Marrow.Warehouse;
using SLZ.Props;
using SLZ.UI;
using System;
using System.IO;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.UI;

namespace iSurvivedBonelab
{
    public class NeedEle
    {
        public SubPanelElement subPanelEle;

        public BoolElement enabledEle;
        public FloatElement decayRateEle;
        public FloatElement startValueEle;
        public FloatElement maxValueEle;

        private Need need;

        public NeedEle(Need need)
        {
            this.need = need;
        }

        public void Create(MenuCategory rootCateg, Color menuColor)
        {
            subPanelEle = rootCateg.CreateSubPanel(need.displayName + " Settings", menuColor);
            enabledEle = rootCateg.CreateBoolElement(need.displayName + " Settings", menuColor, need.prefs.enabledEnt.Value);
            decayRateEle = rootCateg.CreateFloatElement(need.displayName + " Decay Rate", menuColor, need.prefs.decayRateEnt.Value, 0.1f, 0f, float.MaxValue);
            startValueEle = rootCateg.CreateFloatElement(need.displayName + " Start Value", menuColor, need.prefs.startValueEnt.Value, 0.1f, 0f, float.MaxValue);
            maxValueEle = rootCateg.CreateFloatElement(need.displayName + " Max Value", menuColor, need.prefs.maxValueEnt.Value, 0.1f, 0f, float.MaxValue);
        }

        public void Update()
        {
            need.enabled = enabledEle.Value;
            need.decayRate = decayRateEle.Value;
            need.startValue = startValueEle.Value;
            need.maxValue = maxValueEle.Value;
        }
    }

    internal static class MenuStuff
    {
        private static Color _menuColor = new Color(173, 113, 0);

        public static MenuCategory root_categ;

        private static BoolElement _autoEnableEle;

        private static BoolElement _hudEnabledEle;
        private static FloatElement _hudXEle;
        private static FloatElement _hudYEle;
        private static FloatElement _hudZEle;
        private static IntElement _hudHandEle;
        private static IntElement _hudTypeEle;


        internal static void CreateElements()
        {
            // Create root categ
            root_categ = MenuManager.CreateCategory("BLSurvival", _menuColor);

            // HUD settings
            SubPanelElement hud_categ = root_categ.CreateSubPanel("HUD Settings", _menuColor);
            _hudEnabledEle = hud_categ.CreateBoolElement("Hud Enabled", _menuColor, Prefs.hudEnabledEnt.Value);
            _hudXEle = hud_categ.CreateFloatElement("Hud Offset X", _menuColor, Prefs.hudOffsetXEnt.Value, 0.01f, -10f, 10f);
            _hudYEle = hud_categ.CreateFloatElement("Hud Offset Y", _menuColor, Prefs.hudOffsetYEnt.Value, 0.01f, -10f, 10f);
            _hudZEle = hud_categ.CreateFloatElement("Hud Offset Z", _menuColor, Prefs.hudOffsetZEnt.Value, 0.01f, -10f, 10f);
            _hudTypeEle = hud_categ.CreateIntElement("Hud Type", _menuColor, Prefs.hudTypeEnt.Value, 1, 0, 1);
            _hudHandEle = hud_categ.CreateIntElement("Hud Hand", _menuColor, Prefs.hudHandEnt.Value, 1, 0, 1);

            // Create settings subpanels for each system
            // Need Settings
            //NeedsManager.CreateAllEle();

            // Temperature Settings
            SubPanelElement tempur_categ = root_categ.CreateSubPanel("Temperature Settings", _menuColor);
            tempur_categ.CreateFunctionElement("TBD", _menuColor, () => Melon<Main>.Logger.Msg("Player pressed useless FunctionElement!"));

            // Disease Settings
            SubPanelElement disease_categ = root_categ.CreateSubPanel("Disease Settings", _menuColor);
            disease_categ.CreateFunctionElement("TBD", _menuColor, () => Melon<Main>.Logger.Msg("Player pressed useless FunctionElement!"));

            // Create root settings
            _autoEnableEle = root_categ.CreateBoolElement("Enable Mod", _menuColor, true);
        }

        internal static void UpdateSettings()
        {
            // Update the prefs according to the BoneMenu element values

            // Root Prefs
            Prefs.autoEnableEnt.Value = _autoEnableEle.Value;

            // HUD Prefs
            Prefs.hudEnabledEnt.Value = _hudEnabledEle.Value;
            Prefs.hudOffsetXEnt.Value = _hudXEle.Value;
            Prefs.hudOffsetYEnt.Value = _hudYEle.Value;
            Prefs.hudOffsetZEnt.Value = _hudZEle.Value;
            Prefs.hudOffsetEnt.Value = new Vector3(Prefs.hudOffsetXEnt.Value, Prefs.hudOffsetYEnt.Value, Prefs.hudOffsetZEnt.Value);
            Prefs.hudHandEnt.Value = _hudHandEle.Value;
            Prefs.hudTypeEnt.Value = _hudTypeEle.Value;

            // Need Prefs
            //NeedsManager.UpdateAllEle();
        }

        public static GameObject SpawnHud()
        {
            Transform head = Player.playerHead.transform;

            string barcode = "TheUltimateNuke.BLSurvivalAssets.Spawnable.BLSurvivalHUD";
            SpawnableCrateReference reference = new SpawnableCrateReference(barcode);

            Spawnable spawnable = new Spawnable()
            {
                crateRef = reference
            };

            AssetSpawner.Register(spawnable);
            AssetSpawner.Spawn(spawnable, head.position + head.forward, default, new BoxedNullable<Vector3>(Vector3.one), false, new BoxedNullable<int>(null), null, null);

            return GameObject.Find(spawnable.crateRef.Barcode);
        }
    }
}
