using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


namespace RPG.Core
{

    public class MonsterHealth : MonoBehaviour
    {

        public Health enemyHealth;
        public Image fillImage;
        private Slider slider;


        void Awake()
        {
            slider = GetComponent<Slider>();
        }

        // Update is called once per frame
        void Update()
        {
            if (slider.value <= slider.minValue)
            {
                fillImage.enabled = false;
            }

            if (slider.value > slider.minValue && !fillImage.enabled)
            {
                fillImage.enabled = true;
            }

            slider.value = enemyHealth.GetHealth();
        }

    }

}


