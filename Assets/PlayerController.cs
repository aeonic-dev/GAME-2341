using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    Collider2D col;
    Animator ac;
    public static int coins;
    public float speedMax;
    public float speedAccel;
    public float jumpHeight;
    public float heatTime; // max time on hot platforms
    public float velocity;
    public bool grounded;
    public float falloff;
    public float threshold;
    public float soundThreshold;
    public static int lives;
    public int initialLives;

    public AudioClip coin;
    public AudioClip death;

    public AudioSource audioSource;
    public AudioSource slimeSound;
    public AudioSource slimeLand;
    private Boolean doMovement;

    public Boolean hot;
    public static float heat;

    public GameOver gameOver;
    public GameOver gameOver2;

    public Vector3 spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ac = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        velocity = 0;
        grounded = false;
        lives = initialLives;
        audioSource = GetComponent<AudioSource>();
        slimeSound = GetComponents<AudioSource>()[1];
        slimeLand = GetComponents<AudioSource>()[2];
        slimeSound.Play();
        doMovement = true;
        hot = false;
        heat = 0;
        spawnLocation = new Vector3(-7.5f,-2,0);
            Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump") && grounded && doMovement) { // input needs to be done in update, splitting this from main movement is a little janky but apparently necessary
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            grounded = false;
            ac.SetBool("jumping",true);
        }
    }

    void FixedUpdate() { 
        float x = Input.GetAxis("Horizontal");

        if (x != 0 && doMovement) {
            velocity = Mathf.Min(Mathf.Max(speedAccel * x,-speedMax),speedMax);
        }
        velocity *= falloff;

        if (Mathf.Abs(velocity) < threshold) 
            velocity = 0;

        // if (Math.Abs(velocity) < soundThreshold || !grounded)
        //     slimeSound.Pause();
        // else 
        //     slimeSound.UnPause();

        if (grounded)
            slimeSound.volume = Mathf.Min(.5f,(Mathf.Abs(velocity)-threshold)/3);
        slimeSound.volume *= .9f;

        rb.velocity = new Vector2(velocity, rb.velocity.y);
        ac.SetFloat("movement",velocity/2); // adjust for minimum speed to change animation, smaller = more delay

        if (hot) {
            heat = Mathf.Min(heat + 1f / heatTime, 1f);
        }
        if (heat >= 1) {
            Death(null);
        }
    }

    public void OnCollisionEnter2D(Collision2D other) {
        if (!doMovement)
            return;
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Hot") {
            grounded = true;
            ac.SetBool("jumping",false);
            slimeLand.Play();
        }
        if (other.gameObject.tag == "Hot")
            hot = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!doMovement)
            return;
        if (other.gameObject.tag == "Coin") {
            coins += other.gameObject.GetComponent<Coin>().value;
            Destroy(other.gameObject);
            audioSource.PlayOneShot(coin);
        }
        if (other.gameObject.tag == "DeathTrigger")
            Death(other);
        // if (other.gameObject.tag == "EnemyKill") {
        //     slimeLand.PlayOneShot(slimeLand.clip);
        //     Destroy(other.transform.parent.gameObject);
        // }
        if (other.gameObject.tag == "Enemy")
            Death(other);
        if (other.gameObject.tag == "Checkpoint") {
            CheckPoint cp = other.gameObject.GetComponent<CheckPoint>();
            spawnLocation = cp.spawnLocation;
            cp.audioSource.Play();
            heat = 0;
        }
        if (other.gameObject.name == "WinTrigger") {
            gameOver.gameObject.GetComponent<Text>().text = "You win";
            gameOver.Show();
            gameOver2.Show();
            Time.timeScale = 0;
        }
    }

    public void OnCollisionExit2D(Collision2D other) {
        if (!doMovement)
            return;
        if ((other.gameObject.tag == "Ground" || other.gameObject.tag == "Hot") && grounded) {
            grounded = true;
            ac.SetBool("jumping",true);
        }
        if (other.gameObject.tag == "Hot")
            hot = false;
    }

    public void Death(Collider2D col) { // TODO: make this not shitty
        audioSource.PlayOneShot(death);
        velocity = 0;
        rb.velocity = Vector3.zero;
        if (lives > 0) {
            lives --;
            transform.position = spawnLocation;
            heat = 0;
        }
        else {
            ac.SetBool("jumping",false);
            rb.isKinematic = false;
            doMovement = false;
            gameOver.Show();
            gameOver2.Show();
        }
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
