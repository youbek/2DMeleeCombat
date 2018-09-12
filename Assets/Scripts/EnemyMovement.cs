using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    #region PUBLIC_VAR
    public int health = 100;
    public bool hurted = false;
    #endregion

    #region PRIVATE_VAR
    SpriteRenderer body;
    Animator anim;
    bool isDead = false;
    int waitForDestroy = 1;
    bool colorChanged = false;
    #endregion
    // Use this for initialization
    void Start () {
        body = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (hurted && isDead == false)
        {
            StartCoroutine(changeColor(new Vector4(1, 0.3f, 0.3f, 1)));
            anim.SetTrigger("Hurt");
            anim.SetBool("isIdle", false);
            hurted = false;
        } else if(hurted == false && isDead == false)
        {
            anim.ResetTrigger("Hurt");
            anim.SetBool("isIdle", true);
        }

		if(health < 0)
        {
            isDead = true;
            body.color = new Color(1, 1, 1, 1);
            anim.SetTrigger("Hurt");
            StartCoroutine(waitToBeDestory());
            anim.SetBool("isIdle", false);
        }

        if (colorChanged)
        {
            body.color = new Color(1, 1, 1, 1);
            colorChanged = false;
        }

	}

    IEnumerator waitToBeDestory()
    {
        yield return new WaitForSeconds(waitForDestroy);
        Destroy(gameObject);
    }

    IEnumerator changeColor(Vector4 color)
    {
        body.color = new Color(color.x, color.y, color.z, color.w);
        yield return new WaitForSeconds(0.5f);
        colorChanged = true;
    }
}
