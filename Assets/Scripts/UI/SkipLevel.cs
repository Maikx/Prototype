using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipLevel : MonoBehaviour
{
    public Transform checkpoint;
    public GameObject player;
    
    public void SkipToPosition()
    {
        GameManager.instance.RestartPlayerPosition = checkpoint.position;
        SceneManager.LoadScene("Full Scene Tutorial");
        //player.transform.position = checkpoint.position;
    }
}
