using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    private GameObject[] lifeObjects = new GameObject[3];

    void Start()
    {
        lifeObjects[0] = transform.GetChild(0).gameObject;
        lifeObjects[1] = transform.GetChild(1).gameObject;
        lifeObjects[2] = transform.GetChild(2).gameObject;
    }

    public void UpdateCounter(int lives)
    {
        foreach (GameObject obj in lifeObjects)
        {
            obj.SetActive(false);
        }

        for (int i = lifeObjects.Length - lives; i < lifeObjects.Length; i++)
        {
            lifeObjects[i].SetActive(true);
        }
    }
}
