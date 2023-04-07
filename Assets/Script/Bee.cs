using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bee : MonoBehaviour
{
    // Start is called before the first frame updat
    public float speed;
    //public float damage;
    private float horizontal;
    public Rigidbody2D rb;
    private bool isfacing;
    private Animator anime;
    public Transform playerVector2;
    private Vector2 startPos;
    public Transform playerCheck;
    public LayerMask playerLayer;
    public float damage;

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
        if (Vector2.Distance(startPos, transform.position) > 5)
        {
            speed = -speed;
            rb.velocity = new Vector2(speed, rb.velocity.y);

        }
        if (IsPlayerComing() && Vector2.Distance(startPos, transform.position) < 8)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerVector2.position, speed * 3 * Time.deltaTime);
            flip1();
        }
        if(Vector2.Distance(startPos, transform.position) > 8)
        {
            transform.position = Vector3.MoveTowards(transform.position,startPos, speed * Time.deltaTime);
        }

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

    private void flip1()
    {
        if (isfacing && transform.position.x<player.instance.transform.position.x || !isfacing && transform.position.x > player.instance.transform.position.x)
        {
            Vector3 localScale = transform.localScale;
            isfacing = !isfacing;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player a=collision.GetComponent<player>();
        if (a != null)
        {
            a.TakeDamage(damage);
                        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.instance.transform.position.x,transform.position.y,transform.position.z),- speed * 0.4f*Time.deltaTime );

        }
    }
    private bool IsPlayerComing()
    {
        return Physics2D.OverlapCircle(playerCheck.position, 7f, playerLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerCheck.position, 7f);
    }
}
