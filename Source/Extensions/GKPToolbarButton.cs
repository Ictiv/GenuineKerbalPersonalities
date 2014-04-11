using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Toolbar;
using Genuine_Kerbal_Personalities;

namespace Genuine_Kerbal_Personalities.Extensions
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    class GKPToolbarButton : MonoBehaviour
    {
        private IButton GKPButton;
        public static bool redraw = true;

        internal GKPToolbarButton()
        {
            GKPButton = ToolbarManager.Instance.add("GKP", "GKP");
            GKPButton.TexturePath = "SiriusKybernetics/GenuineKerbalPersonalities/Textures/GKP_Button";
            GKPButton.ToolTip = "Toggles Display of Onboard AI Communications";
            GKPButton.OnClick += (e) =>
            {
                if (GenuineKerbalPersonalities.GKPButtonUsable == true)
                {
                    redraw = !redraw;
                }

            };
        }

        internal void OnDestroy()
        {
            GKPButton.Destroy();
        }
    }
}
