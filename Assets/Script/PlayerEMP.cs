using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEMP : MonoBehaviour {

    public Animator anim;

    public void Blast()
    {
        anim.SetTrigger("Blast");
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Enemy")
        {
            target.GetComponent<BaseEnemy>().Explode();
            transform.SetAsLastSibling();
        }
    }	
}
