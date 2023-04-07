using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug : MonoBehaviour
{
    public float speed;
    public float damage;
    private float horizontal;
    public Rigidbody2D rb;
    private bool isfacing;
    private Animator anime;
    public Transform playerVector2;
    private Vector2 startPos;
    public Transform playerCheck;
    public LayerMask playerLayer;



    void Start()
    {
        startPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        flip();

    }

    private void FixedUpdate()
    {

        if (Vector2.Distance(startPos, transform.position) < 5)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);

        }

        else if (Vector2.Distance(startPos, transform.position) > 5)
        {
            speed = -speed;
            rb.velocity = new Vector2(speed, rb.velocity.y);

        }
        /*if (IsPlayerComing())
        {
            transform.position = Vector3.MoveTowards(transform.position, playerVector2.position, speed * 3 * Time.deltaTime);
        }*/




    }

    private void flip()
    {
        if (isfacing && rb.velocity.x < 0 || !isfacing && rb.velocity.x > 0)
        {
            Vector3 localScale = transform.localScale;
            isfacing = !isfacing;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool IsPlayerComing()
    {
        return Physics2D.OverlapCircle(playerCheck.position, 10f, playerLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player a = collision.GetComponent<player>();
        if (a != null)
        {
            a.TakeDamage(damage);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.instance.transform.position.x,transform.position.y,transform.position.z),- speed * 0.4f*Time.deltaTime );
        }
    }
}
