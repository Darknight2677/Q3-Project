using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OriginalMovementScript : MonoBehaviour
{
    bool grounded = false;
    Rigidbody2D rb2;
    SpriteRenderer sr;
    Animator a;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        a = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizValue = Input.GetAxis("Horizontal");
        rb2.velocity = new Vector2(horizValue * 3, rb2.velocity.y);
        a.SetFloat("yVelocity", rb2.velocity.y);
        a.SetBool("grounded", grounded);

        if (horizValue > 0)
        {
            sr.flipX = false;
            a.SetBool("Moving", true);
        }
        if (horizValue < 0)
        {
            sr.flipX = true;
            a.SetBool("Moving", true);
        }
        if(horizValue != 0)
        {
            a.SetBool("Moving", true);
        }
        else
        {
            a.SetBool("Moving", false);
        }

        grounded = Physics2D.BoxCast(transform.position, new Vector2(0.1f, 0.1f), 0, Vector2.down, 1, LayerMask.GetMask("Ground"));
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb2.velocity = new Vector2(rb2.velocity.x, 9);
        }
    }

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(1f);
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "OutOfBounds")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //if (collision.gameObject.tag == "Door1")
        //{

        //}
        //if (collision.gameObject.tag == "Door2")
        //{

        //}
        if (collision.gameObject.tag == "Door3")
        {
            transform.position = new Vector2(-6.39, 2.015);
        }

        if (collision.gameObject.tag == "Door4")
        {
            transform.position = new Vector2(-12.82, -3.985);
        }
    }
}
