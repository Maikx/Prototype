using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image img;
    private int lastHealth;
    public float fadeSpeed;
    public float fadeDuration;
    public int fadeStage;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        fadeStage = 0;
        lastHealth = GameManager.instance.health;
    }

    private void Update()
    {
        CheckDeathStage();
        DeathFade();
    }

    public void CheckDeathStage()
    {
        if(lastHealth != GameManager.instance.health)
        {
            if(GameManager.instance.health == 0)
            {
                fadeStage = 1;
                lastHealth = GameManager.instance.health;
            }
            else
            {
                fadeStage = 2;
                lastHealth = GameManager.instance.health;
            }
        }
    }

    public void DeathFade()
    {
        if (fadeStage == 1)
        {
            StopCoroutine(FadeImage(true));
            StartCoroutine(FadeImage(false));
            fadeStage = 0;
        }
        else if (fadeStage == 2)
        {
            StopCoroutine(FadeImage(false));
            StartCoroutine(FadeImage(true));
            fadeStage = 0;
        }
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = fadeDuration; i >= 0; i -= fadeSpeed * Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= fadeDuration; i += fadeSpeed * Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }
}
