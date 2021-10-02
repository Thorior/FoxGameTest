using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource ASource;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        ASource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void JumpedOn()
    {
        ASource.Play();
        anim.SetTrigger("Death");
       
    }
    public void Death()
    {
        Destroy(this.gameObject);
    }
}
