using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl : MonoBehaviour
{

    public float velocity = 1f;
    private Rigidbody2D myRigidBody;
    public static bool isDead;
    public GameManager gameManager;
    public GameObject deathScreen;
    public Animator birdAnimator;
    void Start()
    {
        myRigidBody = transform.GetComponent<Rigidbody2D>();
        isDead = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetComponent<Rigidbody2D>().IsAwake())
        {
            myRigidBody.velocity = Vector2.up * velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "ScoreDetector")
        {
            gameManager.updateScore();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeathArea")
        {
            isDead = true;
            birdAnimator.SetBool("isDead", true);
            deathScreen.SetActive(true);
            myRigidBody.velocity = Vector2.left * velocity;
            StartCoroutine(endGameWaiter());
        }
    }

    IEnumerator endGameWaiter()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;
    }

}
