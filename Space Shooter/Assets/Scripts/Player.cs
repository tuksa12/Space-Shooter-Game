using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    public float maxRotationY = -30;
    public float maxRotationX = 15;
    public float rotationSpeed = 10f;

    [SerializeField]
    private KeyCode fireButton = KeyCode.Space;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private GameObject playerExplosion;


    public static Text playerStats;
    public static int score = 0;
    public static int lives = 3;
    public static int missed = 0;

    private float shipInvisibleTime = 1.5f;
    private float shipMoveOnToScreenSpeed = 5f;
    private float blinkRate = 0.1f;
    private int numberOfTimesToBlink = 10;
    private int blinkCount;


    enum State
    {
        Playing,
        Explosion,
        Invincible
    };
    private State state = State.Playing;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<Text>();
    }

    public static void UpdadeStats()
    {
        playerStats.text = "Score: " + score.ToString() + "\nLives: " + lives.ToString()
                            + "\nMissed: " + missed.ToString();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (state != State.Explosion)
        {
            // Move player depending on input
            float amtToMoveX = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
            float amtToMoveY = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

            //move the transform
            transform.position += new Vector3(amtToMoveX, amtToMoveY, 0);

            // Screen wrap
            if (transform.position.x < -7.4f)
            {
                transform.position = new Vector3(7.4f, transform.position.y, transform.position.z);
            }
            if (transform.position.x > 7.4f)
            {
                transform.position = new Vector3(-7.4f, transform.position.y, transform.position.z);
            }

            if (transform.position.y < -6f)
            {
                transform.position = new Vector3(transform.position.x, 6f, transform.position.z);
            }
            if (transform.position.y > 6f)
            {
                transform.position = new Vector3(transform.position.x, -6f, transform.position.z);
            }
            //Calculate rotation based on move direction
            Quaternion targetRotation = Quaternion.Euler(Input.GetAxisRaw("Vertical") * maxRotationX, Input.GetAxisRaw("Horizontal") * maxRotationY, 0);

            //set rotation smoothly
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Input.GetKeyDown(fireButton))
            {
                Vector3 position = new Vector3(transform.position.x, transform.position.y + (0.6f * transform.localScale.y), transform.position.z);
                Instantiate(projectilePrefab, position, Quaternion.Euler(0, 0, 0));
            }

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && state == State.Playing)
        {
            //Decrease the player's life and make sure it is shown in the UI
            lives--;
            UpdadeStats();

            StartCoroutine(DestroyShip());

            //Update enemy position
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.SetPositionAndSpeed();

        }
    }

    IEnumerator DestroyShip()
    {
        state = State.Explosion;
        blinkCount = 0;


        //Show explosion
        Instantiate(playerExplosion, transform.position, Quaternion.identity);
        if (lives > 0)
        {
        }
        else
            SceneManager.LoadScene("Lose");
        

        transform.position = new Vector3(0f,-6.2f, transform.position.z);
        yield return new WaitForSeconds(shipInvisibleTime);

        while (transform.position.y < -2.2)
        {
            // Move the ship up
            float amtToMove = shipMoveOnToScreenSpeed * Time.deltaTime;
            transform.position = new Vector3(0, transform.position.y + amtToMove,
            transform.position.z);
            yield return new WaitForEndOfFrame();
        }
        state = State.Invincible;

        while (blinkCount < numberOfTimesToBlink)
        {
            gameObject.GetComponent<Renderer>().enabled = !gameObject.GetComponent<Renderer>().enabled;
            if (gameObject.GetComponent<Renderer>().enabled)
                blinkCount++;
            yield return new WaitForSeconds(blinkRate);
        }
        state = State.Playing;

        
    }
}
