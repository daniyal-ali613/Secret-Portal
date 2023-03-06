using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Core
{
    public class DamageIndicator : MonoBehaviour
    {
        public Image damageIndicatorImage; // Set this in the inspector
        public float maxAlpha = 0.5f;
        public float fadeTime = 0.5f;
        private float alpha = 0f;
        private bool fading = false;

        public float margin;
        private Vector3 damageDirection;

        void Start()
        {
            damageIndicatorImage.gameObject.SetActive(false);
        }

        void Update()
        {
            if (fading)
            {
                alpha -= Time.deltaTime / fadeTime;
                if (alpha <= 0f)
                {
                    alpha = 0f;
                    fading = false;
                    damageIndicatorImage.gameObject.SetActive(false);
                }
                else
                {
                    Color indicatorColor = damageIndicatorImage.color;
                    indicatorColor.a = alpha;
                    damageIndicatorImage.color = indicatorColor;
                }
            }
        }

        public void ShowDamageIndicator(Vector3 direction)
        {
            alpha = maxAlpha;
            fading = true;
            damageDirection = direction;

            // Set the rotation of the damage indicator image to point in the direction of the damage
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            damageIndicatorImage.rectTransform.rotation = Quaternion.Euler(0f, 0f, -angle);

            // Set the position of the damage indicator image to be in the direction of the damage, 100 units away
            Vector3 position = transform.position + direction.normalized * margin;
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);
            damageIndicatorImage.rectTransform.position = screenPosition;

            damageIndicatorImage.gameObject.SetActive(true);
        }
    }

}
