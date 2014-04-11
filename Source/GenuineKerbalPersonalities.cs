using UnityEngine;
using Genuine_Kerbal_Personalities.Extensions;
using System.Collections;
using Toolbar;


namespace Genuine_Kerbal_Personalities
{
    public class GenuineKerbalPersonalities : PartModule
    {
        [KSPField(isPersistant = true)]
        public string OSName;
        public string GivenOSName = "";
        [KSPField(isPersistant = true)]
        public float Personality;
        public float GivenPersonality = 19f;
        [KSPField(isPersistant = true)]
        public bool Booted;
        [KSPField]
        public int PS;

        public float responsetag;
        public static float lastupdate;
        public string message;
        public bool updatenow = false;

        public static bool GKPButtonUsable = false;

        private static Rect _windowposition = new Rect();
        private GUIStyle _windowStyle, _windowStyle1, _windowStyle2, _labelStyle, _labelStyle1, _labelStyle2;
        private bool _hasInitStyles = false;
        
        public override void OnStart(StartState state)
        {
            GKPToolbarButton.redraw = true;
            if (!_hasInitStyles)
                InitStyles();

            if (state != StartState.Editor && Booted)
            {
                GKPButtonUsable = true;
                if (OSName == "SKGKP")
                    setDefaultOSName(Personality);
                responsetag = -1f;
                DrawGKPWindow();
            }


            if (state != StartState.Editor && !Booted)
            {
                responsetag = -1f;
                RenderingManager.AddToPostDrawQueue(0, GKPConfWindow1);
                Booted = true;
            }
            
        }

        private void InitStyles()
        {
            _windowStyle1 = new GUIStyle();
            _windowStyle1.fixedWidth = 250f;
            _windowStyle1.normal.textColor = Color.white;
            _windowStyle2 = new GUIStyle(HighLogic.Skin.window);
            _windowStyle2.fixedWidth = 250f;
            _windowStyle = _windowStyle1;

            _labelStyle1 = new GUIStyle();
            _labelStyle1.stretchWidth = true;
            _labelStyle1.normal.textColor = Color.white;
            _labelStyle2 = new GUIStyle(HighLogic.Skin.label);
            _labelStyle2.stretchWidth = true;
            _labelStyle = _labelStyle1;
            _labelStyle.fontSize

            _hasInitStyles = true;
        }

        private void GKPWindow()
        {
            if (this.vessel == FlightGlobals.ActiveVessel)
            {
                if (GKPToolbarButton.redraw)
                    _windowposition = GUILayout.Window(10, _windowposition, OnGKPWindow, OSName);

                //To add: Saved window position.

                GUI.DragWindow();
            }
                
        }

        private void GKPConfWindow1()
        {
            if (this.vessel == FlightGlobals.ActiveVessel)
            {
                _windowposition = GUILayout.Window(10, _windowposition, OnGKPConfWindow1, "Sirius Kybernetic OS Setup");

                _windowposition = _windowposition.CenterScreen();
            }
        }

        private void GKPConfWindow2()
        {
            if (this.vessel == FlightGlobals.ActiveVessel)
                _windowposition = GUILayout.Window(10, _windowposition, OnGKPConfWindow2, "Sirius Kybernetic OS Setup");
        }

        public void DrawGKPWindow()
        {
            StartCoroutine(Counter());
            RenderingManager.RemoveFromPostDrawQueue(0, new Callback(GKPWindow));
            if (GKPToolbarButton.redraw)
            {
                message = GKPResponses.CallOS(responsetag, Personality, OSName);

                RenderingManager.AddToPostDrawQueue(0, GKPWindow);
            }
        }

        IEnumerator Counter()
        {
            yield return new WaitForSeconds(1f);
        }

        private void OnGKPWindow(int windowid)
        {

            if ((lastupdate + PS) < Time.time)
            {
                lastupdate = Time.time;
                responsetag = 0f;
                updatenow = true;
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Report Resources"))
            {
                responsetag = 1f;
                updatenow = true;

                lastupdate = Time.time;
            }
            if (GUILayout.Button("Report Situation"))
            {
                responsetag = 2f;
                updatenow = true;

                lastupdate = Time.time;
            }
            if (GUILayout.Button("Report Flight Data"))
            {
                responsetag = 3f;
                updatenow = true;

                lastupdate = Time.time;
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Update"))
            {
                updatenow = true;
                responsetag = 0f;
                lastupdate = Time.time;
            }

            GUILayout.Label(message, _labelStyle);

            if (GUILayout.Button("Close Window"))
            {
                GKPToolbarButton.redraw = false;

            }

            if (updatenow)
            {
                updatenow = false;

                DrawGKPWindow();
            }
        }


        private void OnGKPConfWindow1(int windowid)
        {
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;

            GUILayout.BeginHorizontal();
            GUILayout.Label("Welcome to your Genuine Kerbal Personalities(TM) OS Setup!", _labelStyle);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Sirius Kybernetics would like you to please choose a Personality for your OS!", _labelStyle);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if(GUILayout.Button(PersonalityName(1f)))
                GivenPersonality = 1f;
            if(GUILayout.Button(PersonalityName(2f)))
                GivenPersonality = 2f;
            if(GUILayout.Button(PersonalityName(3f)))
                GivenPersonality = 3f;
            GUILayout.EndHorizontal();
            if(GUILayout.Button("Keep Default"))
                GivenPersonality = Personality;
            if (GivenPersonality != 19f)
            {
                Personality = GivenPersonality;
                RenderingManager.RemoveFromPostDrawQueue(0, new Callback(GKPConfWindow1));
                RenderingManager.AddToPostDrawQueue(0, GKPConfWindow2);
            }
        }

        private void OnGKPConfWindow2(int windowid)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Sirus Kybernetics congratulates you on your wise choice!", _labelStyle);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("'" + PersonalityName(Personality) + "' is one of our most popular products!", _labelStyle);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Please give a name to your " + PersonalityName(Personality) + " OS, or leave it to our Sirius Standards(TM)!", _labelStyle);
            GUILayout.EndHorizontal();
            GivenOSName = GUILayout.TextField(GivenOSName);
            if (GUILayout.Button("Use this name!"))
            {
                OSName = GivenOSName;
                GKPButtonUsable = true;
                RenderingManager.RemoveFromPostDrawQueue(0, new Callback(GKPConfWindow2));
                RenderingManager.AddToPostDrawQueue(0, GKPWindow);
                RenderingManager.AddToPostDrawQueue(0, DrawGKPWindow);
            }
            if (GUILayout.Button("Use Sirius Standards(TM)!"))
            {
                //If someone wants to add default names (Or Personalities) to the probe cores, they can, by editing those fields in "Siruis Kybernetics Reconfigurations.cfg" in the Parts folder of SiriusKybernetics/GenuineKerbalPersonalities
                //This snippet checks if there is such a new name, and if there is none, it assigns the Sirius Default.
                if (OSName == "SKGKP")
                    setDefaultOSName(Personality);
                GKPButtonUsable = true;
                RenderingManager.RemoveFromPostDrawQueue(0, new Callback(GKPConfWindow2));
                RenderingManager.AddToPostDrawQueue(0, GKPWindow);
                RenderingManager.AddToPostDrawQueue(0, DrawGKPWindow);
            }

            lastupdate = Time.time;

            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            Vessel.GetMETString
        }

        private string PersonalityName(float num)
        {
            if (num == 1f)
                return "Happy-Go-Lucky";
            if (num == 2f)
                return "Depressing";
            if (num == 3f)
                return "Adventurous";
            else
                return "MissingNo";
        }

        private void setDefaultOSName(float num)
        {
            OSName = "MissingNo";
            if (num == 1f)
                OSName = "Eddie";
            if (num == 2f)
                OSName = "Marvin";
            if (num == 3f)
                OSName = "Adventurous";
        }











        //Booster
        //Booster
        //Booster
        //Booster
        //Booster
        //Booster
        //Booster
    }
}
