    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    #region PUBLIC_VAR

    #endregion
    
    #region PRIVATE_VAR
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private float movementSpeed;
    [SerializeField] private int damage;
    Animator anim;
    bool damageMade;
    #endregion

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        // if mouse left button clicked play attack animation
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isAttacking", true);
        } else
        {
            anim.SetBool("isAttacking", false);
        }

        // check if the attackPoint/damagemaker become acive
        if(attackPoint.gameObject.active == true && damageMade == false)
        {
            damageMade = true;
            Collider2D[] hittedObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius);
            if(hittedObjects.Length > 0)
            {
                for(int i = 0; i < hittedObjects.Length; i++)
                {
                    if(hittedObjects[i].gameObject != gameObject)
                    {
                        EnemyMovement enemy = hittedObjects[i].gameObject.GetComponent<EnemyMovement>();

                        if (enemy != null)
                        {
                            enemy.health -= damage;
                            enemy.hurted = true;
                        }
                    }
                }
            }
        } else if(attackPoint.gameObject.active == false && damageMade == true)
        {
            damageMade = false;
        }

        // Check for axis horizontal
        float movement = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;


        // if movement is not equal to 0, means player pressed a or either d, so stop idling, else stop running
        if (movement != 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking") == false)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
            // if movement float is more than 0 means that it moves to right, so turn player to right and move it
            if (movement > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.Translate(transform.right * movement);
            }
            else if (movement < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.Translate(transform.right * movement);
            }
        } else if(movement == 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking") == false)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", true);
        }
	}

    void OnDrawGizmos()
    {
        if(attackPoint.gameObject.active == false)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        } else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}
