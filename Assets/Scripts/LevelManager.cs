using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject levelText;
    [SerializeField] AudioClip levelSound;
    [SerializeField] GameObject HealthBar;

    [SerializeField] Animator animator;
    float timer;
    void Start()
    {
        levelText.SetActive(false);

        StartCoroutine(levelStarter());
    }
    IEnumerator levelStarter()
    {
        yield return new WaitForSeconds(1.5f);
        levelText.SetActive(true);
        animator.SetTrigger("appear");
        AudioSource.PlayClipAtPoint(levelSound, Camera.main.transform.position, 0.5f);

        yield return new WaitForSeconds(3);
        levelText.SetActive(false);
        HealthBar.SetActive(true);
    }
}
