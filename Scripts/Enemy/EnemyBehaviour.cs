using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehaviour : MonoBehaviour
{
    public PlayerMovement script;
    public float EnemyTerminalSpeed;
    private bool terminalRun = false;
    private bool moved = false;
    private float Zdiff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (script.terminal) { 
            terminalRun = true;
            if (!moved) {
                transform.position = script.playerPos - new Vector3(0, 0, 10);
                moved = true;
            }
        }
        if (terminalRun)
        {
            Zdiff = Mathf.Abs(script.playerPos.z - transform.position.z);
            Debug.Log(Zdiff);
            if (Zdiff > 2) {
                transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * EnemyTerminalSpeed, Space.Self);
            }
        }
    }
}
