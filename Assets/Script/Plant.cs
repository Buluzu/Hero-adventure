using System.Collections;
using System.Collections.Generic;
using static player;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public float damage;
    private bool isfacing;
    public Transform playerCheck;
    public LayerMask playerLayer;
    public Transform playerPos;
    private float TimebtwAttack;
    public float StartTimebtwAttack;
    public Transform attackposition;
    public float attackRange;
    public float sigh;
    public Animator anime;
    [SerializeField] private player test1 ;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerComing()  && test1.HP>0)
        {
            if (TimebtwAttack <= 0f )
            {
                anime.SetBool("Attack", true);   
                Collider2D[] playerinAtkRange = Physics2D.OverlapCircleAll(attackposition.position,attackRange,playerLayer);
                for (int i = 0; i < playerinAtkRange.Length; i++)
                {
                    
                    playerinAtkRange[0].GetComponent<player>().TakeDamage(damage);
                }
                TimebtwAttack = StartTimebtwAttack;
                   
            }
            else
            {
                anime.SetBool("Attack", false);
                TimebtwAttack -= Time.deltaTime;
            }
        }

        flip();


    }

    void flip()
    {
        if (isfacing && playerPos.position.x<transform.position.x || !isfacing && playerPos.position.x > transform.position.x)
        {
            Vector3 localScale = transform.localScale;
            isfacing = !isfacing;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool IsPlayerComing()
    {
        return Physics2D.OverlapCircle(playerCheck.position, sigh, playerLayer);
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackposition.position, attackRange);
    }*/
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackposition.position, sigh);
    }
}
