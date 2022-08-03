using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    //  script temporarily disabled, don’t touch the values!!!!!

    /*
    CheckPoint checkPoint;
    public List<Transform> checkpointTransformList = new List<Transform>();
    public List <GameObject> checkPointInList = new List<GameObject>();      

    public void Start()
    {
        checkPoint = GameObject.FindObjectOfType<CheckPoint>();
    }

    /// <summary>
    /// add an item to the list
    /// </summary>
    /// <param name="checkpointTransform"></param>
    public void AddPositionGameObjectToList(Transform checkpointTransform)  // <- funziona
    {
        checkpointTransformList.Add(checkpointTransform);
    }

    /// <summary>
    /// record the position of the last checkpoint touched
    /// </summary>
    /// <param name="transformCheckPoint"></param>
    /// <returns></returns>
    public Vector2 LastCheckPointTouchedPosition()
    {
        int lastcheckpointactive = checkpointTransformList.Count - 1;
        Vector2 restartPosition =  checkpointTransformList[lastcheckpointactive].transform.position;
        return restartPosition;
    }

    /*
     *  public Vector2 LastCheckPointTouchedPosition(Vector2 transformCheckPoint)
    {
        int lastcheckpointactive = checkpointTransformList.Count - 1;
        Vector2 restartPosition =  checkpointTransformList[lastcheckpointactive].transform.position;
        transformCheckPoint = restartPosition;

        return restartPosition;
    }
    

    // agganciare il primo checkpoint come  oggetto altrimenti l'indice lista va in nagativo
    */
}
