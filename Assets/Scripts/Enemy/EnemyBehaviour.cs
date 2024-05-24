using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public PlayerMovement script;
    public SpawnTile spawnTile;
    public Animator animator1;
    public Animator animator2;
    public Animator animator3;
    bool terminalProcesssStarted;
    private Vector3 playerPosition;
    private bool PlayerRightTurned;
    private bool startRightTerminalProcess;
    private bool startLeftTerminalProcess;
    private bool chaseLeft;
    private bool chaseRight;
    private float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        terminalProcesssStarted = false;
        PlayerRightTurned = false;
        startLeftTerminalProcess = false;
        startRightTerminalProcess = false;
        chaseLeft = false;
        chaseRight = false;
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Game difficulty adjustment
        timeElapsed += Time.deltaTime;
        if (timeElapsed < 4) spawnTile.obstacleSpawnProbability = 0.0f;
        else if (timeElapsed > 4 && timeElapsed <= 15) spawnTile.obstacleSpawnProbability = 0.4f;
        else if (timeElapsed > 15 && timeElapsed <= 30) spawnTile.obstacleSpawnProbability = 0.6f;
        else if (timeElapsed > 30 && timeElapsed <= 60) spawnTile.obstacleSpawnProbability = 0.8f;
        else spawnTile.obstacleSpawnProbability = 0.9f;

        if (script.rightTurnPerformed) PlayerRightTurned = true;
        if (script.leftTurnPerformed) PlayerRightTurned = false;
        playerPosition = script.transform.position;
        if (script.terminal && !terminalProcesssStarted && !script.falling) {
            transform.position = playerPosition;
            transform.localRotation = script.transform.rotation;
            if (PlayerRightTurned) {
                transform.position = playerPosition - new Vector3(8, 0, 0);
                transform.position = transform.position + new Vector3(0, 1f, 0);
                animator1.SetBool("Walk", true);
                animator2.SetBool("Walk", true);
                animator3.SetBool("Walk", true);
                startRightTerminalProcess = true;
            }
            else {
                transform.position = playerPosition - new Vector3(0, 0, 8);
                transform.position = transform.position + new Vector3(0, 1f, 0);
                animator1.SetBool("Walk", true);
                animator2.SetBool("Walk", true);
                animator3.SetBool("Walk", true);
                startLeftTerminalProcess = true;
            }
            terminalProcesssStarted = true;
        }

        if (startLeftTerminalProcess) {
            float diff = Mathf.Abs(transform.position.z - script.transform.position.z);
            if (diff > 4)
            {
                transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime, Space.World);
            }
            else {
                animator1.SetBool("Idle", true);
                animator2.SetBool("Idle", true);
                animator3.SetBool("Idle", true);
            }
        }

        if (startRightTerminalProcess)
        {
            float diff = Mathf.Abs(transform.position.x - script.transform.position.x);
            if (diff > 4)
            {
                transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime, Space.World);
            }
            else {
                animator1.SetBool("Idle", true);
                animator2.SetBool("Idle", true);
                animator3.SetBool("Idle", true);
            }
        }

        if (script.leftCrash) chaseLeft = true;
        if (script.rightCrash || script.crashedNonTerminal) chaseRight = true;

        if (chaseLeft && !spawnTile.obstaclePassed) {
            transform.position = playerPosition;
            transform.localRotation = script.transform.rotation;
            transform.position = script.transform.position - new Vector3(0,0,2);
        }

        if (chaseRight && !spawnTile.obstaclePassed)
        {
            transform.position = playerPosition;
            transform.localRotation = script.transform.rotation;
            transform.position = script.transform.position - new Vector3(2, 0, 0);
        }

        if (spawnTile.obstaclePassed) { 
            chaseLeft = false;
            chaseRight = false;
        }
    }
}
