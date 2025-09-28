using System.Collections;
using UnityEngine;

namespace _ProjectBoy.Scripts.Infostructure
{
    public class LoadingCurtain : MonoBehaviour
    {
        public CanvasGroup curtain;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            FadeOut();
        }

        public void Hide()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            var wait = new WaitForSeconds(0.03f);

            while (curtain.alpha > 0)
            {
                curtain.alpha -= 0.03f;
                yield return wait;
            }

            gameObject.SetActive(false);
        }

        private void FadeOut()
        {
            gameObject.SetActive(true);
            curtain.alpha = 1;
        }
    }
}