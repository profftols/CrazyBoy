using System;
using UnityEngine;

namespace YG
{
    public static partial class YG2
    {
        public static Action<bool> onPauseGame;
        public static bool isPauseGame { get; private set; }

        public static void PauseGame(bool pause, bool editTimeScale, bool editAudioPause, bool editCursor,
            bool editEventSystem)
        {
            if (pause == isPauseGame)
                return;

            if (pause)
            {
                GameplayStop(true);
            }
            else
            {
                if (nowAdsShow)
                    return;

                GameplayStart(true);
            }

            isPauseGame = pause;
            onPauseGame?.Invoke(pause);

            if (infoYG.Basic.autoPauseGame)
            {
                if (pause)
                {
                    var pauseObj = new GameObject { name = "PauseGameYG" };
                    MonoBehaviour.DontDestroyOnLoad(pauseObj);
                    var pauseScr = pauseObj.AddComponent<PauseGameYG>();
                    pauseScr.Setup(editTimeScale, editAudioPause, editCursor, editEventSystem);
                }
                else
                {
                    if (PauseGameYG.inst != null)
                        PauseGameYG.inst.PauseDisabled();
                }
            }
        }

        public static void PauseGame(bool pause)
        {
            PauseGame(pause, true, true, true, infoYG.Basic.editEventSystem);
        }

        public static void PauseGameNoEditEventSystem(bool pause)
        {
            PauseGame(pause, true, true, true, false);
        }
    }
}

#if PLATFORM_WEBGL
namespace YG.Insides
{
    public partial class YGSendMessage
    {
        public void SetPauseGame(string pause)
        {
            YG2.PauseGame(pause == "true");
        }
    }
}
#endif