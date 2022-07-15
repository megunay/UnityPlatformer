using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement { get; private set; }

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();

        if(!Movement)
        {
            Debug.LogError("Check Core Component");
        }
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
    }
}
