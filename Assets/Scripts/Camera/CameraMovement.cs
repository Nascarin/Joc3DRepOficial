using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    public float cameraMoveSpeed = 0; // Should be equal to Player Speed move
    public PlayerMovement script;
    private int terminalAnimCounter = 0;
    private Vector3 centerPos;
    private int turnRotSpeed;
    private int anglePerformed = 0;
    private float xMiddle;
    private int rotationStep = 0;
    private Vector3 directionVector;

    // Start is called before the first frame update
    void Start()
    {
        centerPos = transform.position;
        directionVector = new Vector3(0, 0, 1);
        turnRotSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        cameraMoveSpeed = script.cameraSpeed;
        transform.Rotate(new Vector3(-17, 0, 0), Space.Self);
        transform.Translate(new Vector3(0,0,1) * Time.deltaTime * cameraMoveSpeed, Space.Self);
        transform.Rotate(new Vector3(17, 0, 0), Space.Self);
        if (script.terminal) {
            if (terminalAnimCounter < 5) {
                terminalAnimCounter += 1;
                transform.Translate(new Vector3(0.0f, 0.0f, -1.0f), Space.Self);
            }
        }

        // Camera rotation in case the player turns
        if (script.leftTurnPerformed) {
            Vector3 playerPos = script.playerPos;
            transform.position = playerPos;
            transform.Rotate(new Vector3(-17, 0, 0));
            transform.Rotate(new Vector3(0, -1, 0), Space.Self);
            transform.Translate(new Vector3(0, 8.69f, -10.44f), Space.Self);
            transform.Rotate(new Vector3(17, 0, 0));
            if (rotationStep > 88) {
                script.leftTurnPerformed = false;
                transform.position = playerPos;
                rotationStep = 0;
                transform.Rotate(new Vector3(-17, 0, 0));
                transform.Translate(new Vector3(0, 8.69f, -10.44f), Space.Self);
                transform.Rotate(new Vector3(17, 0, 0));
            }
            else ++rotationStep;
        }

        if (script.rightTurnPerformed)
        {
            Vector3 playerPos = script.playerPos;
            transform.position = playerPos;
            transform.Rotate(new Vector3(-17, 0, 0));
            transform.Rotate(new Vector3(0, 1, 0), Space.Self);
            transform.Translate(new Vector3(0, 8.69f, -10.44f), Space.Self);
            transform.Rotate(new Vector3(17, 0, 0));
            if (rotationStep > 88)
            {
                script.rightTurnPerformed = false;
                transform.position = playerPos;
                rotationStep = 0;
                transform.Rotate(new Vector3(-17, 0, 0));
                transform.Translate(new Vector3(0, 8.69f, -10.44f), Space.Self);
                transform.Rotate(new Vector3(17, 0, 0));
            }
            else ++rotationStep;
        }
    }
}
