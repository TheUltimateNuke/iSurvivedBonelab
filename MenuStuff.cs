using BoneLib;
using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;
using MelonLoader;
using SLZ.Interaction;
using System.IO;
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
        private static Color menuColor = new Color(173, 113, 0);

        public static GameObject menuAsset;

        private static MenuCategory root_categ;

        private static BoolElement autoEnableEle;

        private static BoolElement hudEnabledEle;
        private static FloatElement hudXEle;
        private static FloatElement hudYEle;
        private static FloatElement hudZEle;
        private static IntElement hudHandEle;
        private static IntElement hudTypeEle;


        internal static void CreateElements()
        {
            // Create root categ
            root_categ = MenuManager.CreateCategory("BLSurvival", menuColor);

            // HUD settings
            SubPanelElement hud_categ = root_categ.CreateSubPanel("HUD Settings", menuColor);
            hudEnabledEle = hud_categ.CreateBoolElement("Hud Enabled", menuColor, Prefs.hudEnabledEnt.Value);
            hudXEle = hud_categ.CreateFloatElement("Hud Offset X", menuColor, Prefs.hudOffsetXEnt.Value, 0.01f, -10f, 10f);
            hudYEle = hud_categ.CreateFloatElement("Hud Offset Y", menuColor, Prefs.hudOffsetYEnt.Value, 0.01f, -10f, 10f);
            hudZEle = hud_categ.CreateFloatElement("Hud Offset Z", menuColor, Prefs.hudOffsetZEnt.Value, 0.01f, -10f, 10f);
            hudTypeEle = hud_categ.CreateIntElement("Hud Type", menuColor, Prefs.hudTypeEnt.Value, 1, 0, 1);
            hudHandEle = hud_categ.CreateIntElement("Hud Hand", menuColor, Prefs.hudHandEnt.Value, 1, 0, 1);

            // Create settings subpanels for each system
            // Hunger Settings
            NeedsStuff.hungerNeed.ele.Create(root_categ, menuColor);

            // Thirst Settings
            NeedsStuff.thirstNeed.ele.Create(root_categ, menuColor);

            // Temperature Settings
            SubPanelElement tempur_categ = root_categ.CreateSubPanel("Temperature Settings", menuColor);
            tempur_categ.CreateFunctionElement("TBD", menuColor, () => Melon<Main>.Logger.Msg("Player pressed useless FunctionElement!"));

            // Disease Settings
            SubPanelElement disease_categ = root_categ.CreateSubPanel("Disease Settings", menuColor);
            disease_categ.CreateFunctionElement("TBD", menuColor, () => Melon<Main>.Logger.Msg("Player pressed useless FunctionElement!"));

            // Create root settings
            autoEnableEle = root_categ.CreateBoolElement("Enable Mod", menuColor, true);
        }

        // Instantiate the BLSurvival HUD
        internal static void CreateHud()
        {
            var resourceName = "iSurvivedBonelab.Assets.blsurvivalhud";

            using (Stream stream = Main.assembly.GetManifestResourceStream(resourceName))
            {
                // Load Embedded AssetBundle
                using (MemoryStream memStream = new MemoryStream())
                {
                    stream.CopyTo(memStream);
                    byte[] memStreamArray = memStream.ToArray();
                    AssetBundle localAssetBundle = AssetBundle.LoadFromMemory(memStreamArray);

                    // Display error if something went wrong
                    if (localAssetBundle == null) Melon<Main>.Logger.Error("failed to load BLSurvivalHUD AssetBundle. Please make sure you have your stuff set up correctly.");

                    // Instantiate the HUD
                    Object menuAssetObj = localAssetBundle.LoadAsset("BLSurvivalHUD");
                    menuAsset = GameObject.Instantiate(menuAssetObj.Cast<GameObject>());

                    // Unload the AssetBundle
                    localAssetBundle.Unload(false);
                }
            }
        }

        // B)
        private static void DestroyHud()
        {
            if (menuAsset != null) GameObject.Destroy(menuAsset);
        }

        // OnUpdate in relation to HUD settings
        internal static void UpdateSettings()
        {
            // Update the prefs according to the BoneMenu element values

            // Root Prefs
            Prefs.autoEnableEnt.Value = autoEnableEle.Value;

            // HUD Prefs
            Prefs.hudEnabledEnt.Value = hudEnabledEle.Value;
            Prefs.hudOffsetXEnt.Value = hudXEle.Value;
            Prefs.hudOffsetYEnt.Value = hudYEle.Value;
            Prefs.hudOffsetZEnt.Value = hudZEle.Value;
            Prefs.hudOffsetEnt.Value = new Vector3(Prefs.hudOffsetXEnt.Value, Prefs.hudOffsetYEnt.Value, Prefs.hudOffsetZEnt.Value);
            Prefs.hudHandEnt.Value = hudHandEle.Value;
            Prefs.hudTypeEnt.Value = hudTypeEle.Value;

            // Hunger Prefs
            NeedsStuff.hungerNeed.prefs.Update();
            NeedsStuff.hungerNeed.ele.Update();

            // Thirst Prefs
            NeedsStuff.hungerNeed.prefs.Update();
            NeedsStuff.thirstNeed.ele.Update();
        }

        // Parent the hud to the set hudHand
        private static void MoveHud(int hudHand)
        {
            Hand hand = Player.leftHand;
            if (hudHand == 1) hand = Player.rightHand;

            menuAsset.transform.parent = hand.transform;
            menuAsset.transform.position = hand.transform.position + Prefs.hudOffsetEnt.Value;
            menuAsset.transform.rotation = hand.transform.rotation;
        }

        // OnUpdate in relation to HUD elements
        internal static void UpdateHud()
        {
            if (menuAsset != null && Prefs.hudEnabledEnt.Value)
            {
                if (Player.handsExist) MoveHud(Prefs.hudHandEnt.Value);

                //FoodGauge();
                //WaterGauge();
                HudGauges(GameObject.Find("Gauges").transform);
            }
            else if (menuAsset == null && Prefs.hudEnabledEnt.Value) CreateHud();
            else DestroyHud();
        }

        private static void HudGauges(Transform gaugeContainer)
        {
            Melon<Main>.Logger.Msg("Updating hud gauges. gaugeContainer = " + gaugeContainer.name);
            Transform foodGauge = GameObject.Find("FoodGauge").transform;
            Transform thirstGauge = GameObject.Find("ThirstGauge").transform;

            if (foodGauge != null && thirstGauge != null)
            {
                switch (Prefs.hudTypeEnt.Value)
                {
                    case 0:
                        // hunger bar
                        if (NeedsStuff.hungerNeed.enabled)
                        {
                            Image hungerGaugeBar = foodGauge.GetComponentInChildren<Image>();
                            foodGauge.GetComponentInChildren<Slider>().transform.parent.gameObject.SetActive(false);
                            hungerGaugeBar.transform.parent.gameObject.SetActive(true);
                            hungerGaugeBar.fillAmount = NeedsStuff.hungerNeed.GetPercentage();
                        }
                        else foodGauge.gameObject.SetActive(false);

                        // thirst bar
                        if (NeedsStuff.thirstNeed.enabled)
                        {
                            Image thirstGaugeBar = thirstGauge.GetComponentInChildren<Image>();
                            thirstGauge.GetComponentInChildren<Slider>().transform.parent.gameObject.SetActive(false);
                            thirstGaugeBar.transform.parent.gameObject.SetActive(true);
                            thirstGaugeBar.fillAmount = NeedsStuff.thirstNeed.GetPercentage();
                        }
                        else thirstGauge.gameObject.SetActive(false);

                        break;
                    case 1:
                        // hunger bar 2
                        if (NeedsStuff.hungerNeed.enabled)
                        {
                            Slider hungerGaugeBar = foodGauge.GetComponentInChildren<Slider>();
                            foodGauge.GetComponentInChildren<Image>().transform.parent.gameObject.SetActive(false);
                            foodGauge.GetComponentInChildren<RawImage>().transform.parent.gameObject.SetActive(false);
                            hungerGaugeBar.transform.parent.gameObject.SetActive(true);
                            hungerGaugeBar.value = NeedsStuff.hungerNeed.GetPercentage();
                        }
                        else foodGauge.gameObject.SetActive(false);

                        // thirst bar 2
                        if (NeedsStuff.thirstNeed.enabled)
                        {
                            Slider thirstGaugeBar = thirstGauge.GetComponentInChildren<Slider>();
                            thirstGauge.GetComponentInChildren<Image>().transform.parent.gameObject.SetActive(false);
                            thirstGauge.GetComponentInChildren<RawImage>().transform.parent.gameObject.SetActive(false);
                            thirstGaugeBar.transform.parent.gameObject.SetActive(true);
                            thirstGaugeBar.value = NeedsStuff.thirstNeed.GetPercentage();
                        }
                        else thirstGauge.gameObject.SetActive(false);

                        break;
                }
            }
        }
    }
}
