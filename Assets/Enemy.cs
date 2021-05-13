using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool grounded;
    public float jumpHeight;

    AudioSource slimeLand;
    Rigidbody2D rb;
    Collider2D col;
    public Collider2D top;
    Animator ac;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ac = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        grounded = false;
        slimeLand = GetComponents<AudioSource>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Hot") {
            grounded = true;
            ac.SetBool("jumping",false);
            slimeLand.Play();
            StartCoroutine("Jump");
        }
    }

    IEnumerator Jump() {
        yield return new WaitForSeconds(.6f);

        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        grounded = false;
        ac.SetBool("jumping",true);
    }

    public void Death() {
        Debug.Log("dead");
        Destroy(this);
    }
}
