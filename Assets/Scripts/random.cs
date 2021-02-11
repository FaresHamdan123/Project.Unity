using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class random : MonoBehaviour
{
    public static random instance;
    public GameObject []fruits;
    float maxPos = 5.8f;
    float minPos = -5.8f;
    float YPos = 0.116f;
    void Awake()
    {
        MakeInstace();
       
    }

     void MakeInstace()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    
}
