namespace YG
{
    public partial interface IPlatformsYG2
    {
        void GameplayStart()
        {
        }

        void GameplayStop()
        {
        }

        void HappyTime()
        {
        }
    }

    public static partial class YG2
    {
        public static bool isGameplaying { get; private set; }

        private static bool saveGameplayState;

        public static void GameplayStart(bool useSaveGameplayState = false)
        {
            if (useSaveGameplayState && (!saveGameplayState || nowAdsShow || !isFocusWindowGame))
                return;

            if (!isGameplaying)
            {
                Message("Gameplay Start");
                isGameplaying = true;
                iPlatform.GameplayStart();
            }
        }

        public static void GameplayStop(bool useSaveGameplayState = false)
        {
            if (useSaveGameplayState && !nowAdsShow && isFocusWindowGame)
                saveGameplayState = isGameplaying;

            if (isGameplaying)
            {
                Message("Gameplay Stop");
                isGameplaying = false;
                iPlatform.GameplayStop();
            }
        }
    }
}