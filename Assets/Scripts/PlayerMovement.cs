using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float jumpSpeed = 5;
    public float spinSpeed = 1;

    Vector3 startingPosition;

    bool canRoll = true;
    bool jumpKey;
    bool restartKey;
    bool isJumping = false;
    bool canRestart = false;

    Rigidbody2D rb;
    public GameObject floor;
    public GameObject spikeSpawner;
    public GameObject obstacles;
    public GameObject restartScreen;

    void Start()
    {
        startingPosition = gameObject.transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        restartScreen.SetActive(false);
    }

    void Update()
    {
        getInput();

        if(jumpKey && !isJumping)
        {
            jump();
        }

        //make the ball "roll" across the ground
        if(canRoll)
        {
            rb.rotation -= spinSpeed;
        }
        if(restartKey && canRestart)
        {
            SceneManager.LoadScene("Level 1");
        }
        
    }

    void getInput()
    {
#if UNITY_EDITOR_WIN
        Debug.Log("ur on unity editor windows");
        keyboardInput();
#endif
#if UNITY_ANDROID
        Debug.Log("ur on android");
        touchScreenInput();
#endif
    }

    void touchScreenInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (canRestart)
                {
                    SceneManager.LoadScene("Level 1");
                }
                else
                jump();
                
            }
        }
    }

    void keyboardInput()
    {
        jumpKey = Input.GetKeyDown(KeyCode.Space);
        restartKey = Input.GetKeyDown(KeyCode.Return);
    }

    void jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }

   
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Floor")
        {
            isJumping = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spike Collision")
        {
            Scoring.instance.AddScore();
            GameManager.instance.SpawnObject(); 
            //spawns obstacles after scoring a point
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if this player "dies"
        if (collision.gameObject.tag == "Spike")
        {
            Destroy(floor);
            Destroy(spikeSpawner);
            Destroy(obstacles);

            canRestart = true;
            canRoll = false;
            if (canRestart)
            {
                restartScreen.SetActive(true);
            }
        }

        //deleting player after they leave the camera
        if (gameObject.transform.position.y <= -16)
        {
            //Destroy(gameObject);
        }
    }
}
