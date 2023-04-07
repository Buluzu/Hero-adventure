using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static Enemies;

public class Slash : MonoBehaviour
{
    public float life = 3;
    public SpriteRenderer slashf;
    public float SlashDamage;
    public float slashSpeed;
    public float SpeedRate;
    private Rigidbody2D rb;
    public static Slash instance;

    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity= transform.right*slashSpeed*SpeedRate;
    }
    private void Awake()
    {
        Destroy(gameObject,life);
        instance= this;
    }
   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);    
    }*/
    private void OnTriggerEnter2D(Collider2D hitinfor)
    {
        Enemies a = hitinfor.GetComponent<Enemies>();
        if (a != null)
        {
            a.EnemiesTakeDamage(player.instance.SlashDamage);
            Destroy(gameObject);
        }
        
    }

}
