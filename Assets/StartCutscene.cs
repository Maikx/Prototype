using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCutscene : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D target)
    {
        SceneManager.LoadScene("CutsceneFinale");
    }
}
