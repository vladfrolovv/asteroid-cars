#region

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

#endregion

namespace Game.Runtime.UtilitiesContainer
{
    public static class Utilities
    {
        public static IEnumerator Wait(Action callback)
        {
            yield return null;
            callback?.Invoke();
        }


        public static IEnumerator Wait(Action callback, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            callback?.Invoke();
        }


        public static IEnumerator Animate(Action<float> update, float duration, Action onEnd = null, float startingT = 0f)
        {
            float start = Time.time - startingT * duration;
            float t;
            while ((t = (Time.time - start) / duration) <= 1f)
            {
                update(t);
                yield return null;
            }

            update(1f);

            onEnd?.Invoke();
        }


        public static IEnumerator Animate(float duration, Action onEnd = null, params Action<float>[] Updates)
        {
            return Animate(t =>
            {
                foreach (Action<float> Update in Updates)
                {
                    if (Update != null)
                    {
                        Update(t);
                    }
                }
            }, duration, onEnd);
        }


        public static void SetAlpha(this Image renderer, float alpha)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, alpha);
        }

        public static void SetAlpha(this SpriteRenderer renderer, float alpha)
        {
            Color color = renderer.color;
            renderer.color = new Color(color.r, color.g, color.b, alpha);
        }

        public static Action<float> Fade(CanvasGroup canvasGroup, float fadeTo)
        {
            if (canvasGroup == null)
                return null;

            float fadeFrom = canvasGroup.alpha;
            return t => { canvasGroup.alpha = Lerp(fadeFrom, fadeTo, t); };
        }


        public static Action<float> Fade(CanvasGroup canvasGroup, float fadeFrom, float fadeTo)
        {
            if (canvasGroup == null)
                return null;

            return t => { canvasGroup.alpha = Lerp(fadeFrom, fadeTo, t); };
        }



        public static Action<float> Fade(List<CanvasGroup> canvasGroups, float fadeTo)
        {
            if (canvasGroups == null)
                return null;

            float fadeFrom = canvasGroups[0].alpha;
            return t => { canvasGroups.ForEach(group => { group.alpha = Lerp(fadeFrom, fadeTo, t); }); };
        }


        public static Action<float> Fade(Image image, float fadeFrom, float fadeTo)
        {
            if (image == null)
                return null;

            Color color = image.color;
            return t => { image.color = new Color(color.r, color.g, color.b, Lerp(fadeFrom, fadeTo, t)); };
        }


        public static Action<float> Fade(Image image, float fadeTo)
        {
            if (image == null)
                return null;

            Color color = image.color;
            return Fade(image, color.a, fadeTo);
        }


        public static Action<float> Fade(MaskableGraphic image, float fadeTo)
        {
            if (image == null)
                return null;

            Color color = image.color;
            float fadeFrom = image.color.a;
            return t => { image.color = new Color(color.r, color.g, color.b, Lerp(fadeFrom, fadeTo, t)); };
        }


        public static Action<float> Translate(RectTransform rectTransform, Vector2 moveTo)
        {
            if (rectTransform == null)
                return null;

            Vector2 moveFrom = rectTransform.anchoredPosition;
            return t => { rectTransform.anchoredPosition = Lerp(moveFrom, moveTo, t); };
        }


        public static Action<float> Translate(RectTransform rectTransform, Vector2 moveFrom, Vector2 moveTo)
        {
            if (rectTransform == null)
                return null;

            return t => { rectTransform.anchoredPosition = Lerp(moveFrom, moveTo, t); };
        }


        public static Action<float> Translate(Transform gameObject, Vector3 moveTo, bool local = true)
        {
            if (gameObject == null)
                return null;

            Vector3 moveFrom = local ? gameObject.localPosition : gameObject.position;
            return Translate(gameObject, moveFrom, moveTo, local);
        }


        public static Action<float> Translate(Transform gameObject, Vector3 moveFrom, Vector3 moveTo, bool local = true)
        {
            if (gameObject == null)
                return null;

            return t =>
            {
                if (local)
                    gameObject.transform.localPosition = Lerp(moveFrom, moveTo, t);
                else
                    gameObject.transform.position = Lerp(moveFrom, moveTo, t);
            };
        }


        public static Action<float> Scale(GameObject gameObject, Vector3 scaleFrom, Vector3 scaleTo)
        {
            if (gameObject == null)
                return null;

            return t => { gameObject.transform.localScale = Lerp(scaleFrom, scaleTo, t); };
        }

        public static Action<float> Scale(GameObject gameObject, Vector3 scaleTo)
        {
            if (gameObject == null)
                return null;

            Vector3 scaleFrom = gameObject.transform.localScale;
            return Scale(gameObject, scaleFrom, scaleTo);
        }

        public static Action<float> Scale(Transform transform, Vector3 scaleFrom, Vector3 scaleTo)
        {
            if (transform == null)
                return null;

            return t => { transform.localScale = Lerp(scaleFrom, scaleTo, t); };
        }

        public static Action<float> Scale(Transform transform, Vector3 scaleTo)
        {
            if (transform == null)
                return null;

            Vector3 scaleFrom = transform.localScale;
            return Scale(transform, scaleFrom, scaleTo);
        }


        public static Action<float> Scale(RectTransform gameObject, Vector3 scaleFrom, Vector3 scaleTo)
        {
            if (gameObject == null)
                return null;

            return t => { gameObject.localScale = Lerp(scaleFrom, scaleTo, t); };
        }


        public static Action<float> Scale(RectTransform gameObject, Vector3 scaleTo)
        {
            if (gameObject == null)
                return null;

            Vector3 scaleFrom = gameObject.localScale;
            return t => { gameObject.localScale = Lerp(scaleFrom, scaleTo, t); };
        }


        public static Action<float> Rotate(Transform transform, Vector3 rotateTo)
        {
            if (transform == null)
                return null;

            Vector3 rotateFrom = transform.localEulerAngles;
            return t => { transform.localEulerAngles = Lerp(rotateFrom, rotateTo, t); };
        }

        public static Action<float> PrintText(TextMeshProUGUI textField, string text)
        {
            return t => textField.text = text?[..(int)(t * text.Length)];
        }

        public static float Lerp(float from, float to, float t)
        {
            return from + (to - from) * t;
        }


        public static Vector3 Lerp(Vector3 from, Vector3 to, float t)
        {
            return from + (to - from) * t;
        }
    }


    public static class DeviceUtilities
    {
        private static float DeviceDiagonalSizeInInches()
        {
            float screenWidth = Screen.width / Screen.dpi;
            float screenHeight = Screen.height / Screen.dpi;
            float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

            return diagonalInches;
        }


        public static bool IsTablet()
        {
// #if UNITY_IOS
//             return UnityEngine.iOS.Device.generation.ToString().Contains("iPad");
// #elif UNITY_ANDROID
//             float aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
//             return DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f;
// #endif
            float aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
            return DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f;
        }
    }

}
