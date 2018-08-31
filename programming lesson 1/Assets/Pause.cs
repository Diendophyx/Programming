using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool paused;

    void Start()
    {
        Time.timeScale = 1;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Time.timeScale = 1;
                paused = false;
            }
            else
            {
                Time.timeScale = 0;
                paused = true;
            }
        }
    }
}

