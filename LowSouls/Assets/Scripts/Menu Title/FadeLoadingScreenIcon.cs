using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LS {
    public class FadeLoadingScreenIcon : MonoBehaviour
    {
        [SerializeField] Image fadeImage;
        private Coroutine fadeCoroutine;

        private void OnEnable()
        {
            FadeUIImage();
        }

        private void OnDisable()
        {
            if (fadeCoroutine != null) 
                StopCoroutine(fadeCoroutine);
        }

        public void FadeUIImage()
        {
            if (fadeCoroutine != null) 
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeCoroutine(true));
        }

        private IEnumerator FadeCoroutine(bool fade)
        {
            if (fade)
            {
                for (float i = 1; i >= 0; i -= Time.unscaledDeltaTime)
                {
                    fadeImage.color = new Color(1, 1, 1, i);
                    yield return null;
                }
                fadeCoroutine = StartCoroutine(FadeCoroutine(false));
            }
            else
            {
                for (float i = 0; i <= 1; i += Time.unscaledDeltaTime)
                {
                    fadeImage.color = new Color(1, 1, 1, i);
                    yield return null;
                }
                fadeCoroutine = StartCoroutine(FadeCoroutine(true));
            }
        }
    }
}
