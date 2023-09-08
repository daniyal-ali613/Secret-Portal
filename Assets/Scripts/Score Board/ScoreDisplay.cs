using System.Collections;
using System.Collections.Generic;
using TMPro;
using RPG.Core;
using UnityEngine;

namespace RPG.GameInfo
{
    public class ScoreDisplay : MonoBehaviour
    {
        TextMeshProUGUI playerHealth;
        public Health health;
        // Start is called before the first frame update
        void Start()
        {
            playerHealth = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            playerHealth.text = health.GetHealth().ToString();
        }
    }
}

