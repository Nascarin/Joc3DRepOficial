using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public BoxCollider hitbox;
    public BoxCollider trigHitbox;
    private float speedNerfTimer = 0;
    public float RunSpeed;
    private float idleRunSpeed;
    private float CrashSpeed;
    public float cameraSpeed;
    float initialRunSpeed;
    public float HorizontalSpeed = 0;
    public float LeftBound = 0;
    public float RightBound = 0;
    float horiScale = 0;
    public float jumpIntensity = 0;
    bool canJump = true;
    private float jumpCooldown = 0;
    bool canCrawl = true;
    public bool leftCrash = false;
    public bool rightCrash = false;
    public bool enemyFollow = false;
    bool startCrawlTimer = false;
    bool hasTurned = false;
    public bool leftTurnPerformed = false;
    public bool rightTurnPerformed = false;
    public bool falling = false;
    public bool terminal = false;
    float crawlTimer = 0;
    float turnCooldown = 0;
    float lateralCrashCooldown = 0;
    float nonFinishingCooldown = 0;
    float newLateralCrashCooldown = 0;
    public float jumpDiff = 0;
    private Rigidbody rb;
    private float floorY;
    public bool crashedNonTerminal;
    public Vector3 playerPos;
    public Vector3 initialPos;
    public EndRunSequence endRunSequence;
    public int coinsCollected;
    public int pointsCollected;
    private float pointsTime;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        hitbox = GetComponent<BoxCollider>();
        //trigHitbox = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        floorY = transform.localPosition.y;
        initialRunSpeed = RunSpeed;
        cameraSpeed = RunSpeed;
        initialPos = transform.position;
        CrashSpeed = RunSpeed * 0.5f;
        idleRunSpeed = RunSpeed;
        crashedNonTerminal = false;
        coinsCollected = 0;
        pointsCollected = 0;
        pointsTime = 0;
    }

    // Collision detection
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "FRObstacle") {
            animator.SetBool("FrontalCrash", true);
            RunSpeed = 0;
            cameraSpeed = 0;
            terminal = true;
            endRunSequence.StartEndSequence();
        }
        if (collision.gameObject.tag == "LeftWall") {
            if (lateralCrashCooldown == 0 || lateralCrashCooldown > 1) {
                if (leftCrash || rightCrash)
                {
                    RunSpeed = 0;
                    cameraSpeed = 0;
                    terminal = true;
                }
                else
                {
                    leftCrash = true;
                    enemyFollow = true;
                    Debug.Log("LEFT-WALL-TOUCHED!!");
                    if (lateralCrashCooldown == 0) RunSpeed = CrashSpeed;
                }
            }
        }
        if (collision.gameObject.tag == "RightWall")
        {
            if (lateralCrashCooldown == 0 || lateralCrashCooldown > 1) {
                if (rightCrash || leftCrash)
                {
                    RunSpeed = 0;
                    cameraSpeed = 0;
                    terminal = true;
                }
                else
                {
                    rightCrash = true;
                    enemyFollow = true;
                    Debug.Log("RIGHT-WALL-TOUCHED!!");
                    if (lateralCrashCooldown == 0) RunSpeed = CrashSpeed;
                }
            }
        }
        if (collision.gameObject.tag == "NonFOBS") {
            if (crashedNonTerminal)
            {
                RunSpeed = 0;
                cameraSpeed = 0;
                terminal = true;
                animator.SetBool("Injured", false);
            }
            else {
                Quaternion rot = transform.rotation; // Safety measures: Avoiding unexpected behaviour from the physics engine
                transform.Translate(new Vector3(0, 0, 1.4f), Space.Self);
                transform.rotation = rot;
                crashedNonTerminal = true;
                if (nonFinishingCooldown == 0) RunSpeed = CrashSpeed;
                animator.SetBool("Injured", true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin") {
            ++coinsCollected;
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        cameraSpeed = RunSpeed;
        trigHitbox.center = hitbox.center;
        trigHitbox.size = hitbox.size;
        playerPos = transform.position;

        // Time points counting at every quarter of a second
        pointsTime += Time.deltaTime;
        if (pointsTime > 0.5f) {
            ++pointsCollected;
            pointsTime = 0;
        }
        // Idle forward running
        transform.Translate(new Vector3(0.0f,0.0f,1.0f) * Time.deltaTime * RunSpeed, Space.Self);

        // Moving to the left
        if (Input.GetKey(KeyCode.LeftArrow)) {
                transform.Translate(new Vector3(-1.0f, 0.0f, 0.0f) * Time.deltaTime * HorizontalSpeed, Space.Self);
        }

        // Moving to the right
        if (Input.GetKey(KeyCode.RightArrow))
        {
                transform.Translate(new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime * HorizontalSpeed, Space.Self);
        }

        // Jumping
        if (canJump && Input.GetKey(KeyCode.UpArrow)) {
            Debug.Log("JUMPING-SETTING-ANIM");
            animator.SetBool("isJumping", true);
            canJump = false;
            rb.AddForce(Vector3.up * jumpIntensity, ForceMode.Impulse);
            hitbox.center = new Vector3(0, 1.88f, 0);
        }
        if (!canJump) {
            if (jumpCooldown > 1 && (transform.localPosition.y - floorY) < jumpDiff) animator.SetBool("isJumping", false);
            if (/*(transform.localPosition.y - floorY) < jumpDiff && */jumpCooldown > 1.5f)
            {
                canJump = true;
                //hitbox.center = new Vector3(0, 0.82f, 0);
                jumpCooldown = 0;
            }
            else jumpCooldown += Time.deltaTime;
            if (jumpCooldown > 1) {
                hitbox.center = new Vector3(0, 0.82f, 0);
            }
        }

        // Crawling
        if (canCrawl && Input.GetKey(KeyCode.DownArrow)) {
            animator.SetBool("isCrawling", true);
            canCrawl = false;
            startCrawlTimer = true;
            hitbox.center = new Vector3(0, 0.68f, 0);
            hitbox.size = new Vector3(1, 0.95f, 1);
        }
        if (startCrawlTimer && crawlTimer < 1.2)
        {
            crawlTimer += Time.deltaTime;
        }
        else if (crawlTimer > 1.2){
            startCrawlTimer = false;
            crawlTimer = 0;
            canCrawl = true;
            hitbox.center = new Vector3(0, 0.82f, 0);
            hitbox.size = new Vector3(1, 1.62f, 1);
            animator.SetBool("isCrawling", false);
        }

        // Check left side wall crash
        if (leftCrash && !terminal) {
            if (lateralCrashCooldown > 5)
            {
                leftCrash = false;
                RunSpeed = idleRunSpeed;
                lateralCrashCooldown = 0;
                enemyFollow = false;
            }
            else lateralCrashCooldown += Time.deltaTime;
        }


        // Check left side wall crash
        if (rightCrash && !terminal)
        {
            if (lateralCrashCooldown > 5)
            {
                rightCrash = false;
                RunSpeed = idleRunSpeed;
                lateralCrashCooldown = 0;
                enemyFollow = false;
            }
            else lateralCrashCooldown += Time.deltaTime;
        }

        // Check crash with non-terminal obstacles
        if (crashedNonTerminal && !terminal) {
            if (nonFinishingCooldown > 5)
            {
                crashedNonTerminal = false;
                RunSpeed = idleRunSpeed;
                nonFinishingCooldown = 0;
                animator.SetBool("Injured", false);
            }
            else nonFinishingCooldown += Time.deltaTime;
        }

        // Turn Right
        if (!hasTurned && Input.GetKey(KeyCode.D)) {
            rightTurnPerformed = true;
            transform.Rotate(new Vector3(0, 90, 0), Space.Self);
            hasTurned = true;
        }

        // Turn Left
        if (!hasTurned && Input.GetKey(KeyCode.A))
        {
            leftTurnPerformed = true;
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            hasTurned = true;
        }

        // Turn Cooldown to avoid multiple key-press detection
        if (hasTurned) {
            turnCooldown += Time.deltaTime;
            if (turnCooldown > 0.5f) {
                turnCooldown = 0;
                hasTurned = false;
                animator.SetBool("rightTurning", false);
            }
        }

        // Falling Down (Game losing state)
        if (transform.localPosition.y < (floorY - 2))
        {
            falling = true;
            animator.SetBool("isFalling", true);
            terminal = true;
            endRunSequence.StartEndSequence();
        }
        else falling = false;
    }
}
