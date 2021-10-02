using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectible : MonoBehaviour
{
    protected Animator Anim;
    public enum AnimFSM {idle, pickedup};
    public AnimFSM currentState = AnimFSM.idle;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public virtual void StateCheck(AnimFSM currentState)
    {
        if (currentState == AnimFSM.pickedup)
        {
            Anim.SetInteger("State", (int)currentState);
        }
        else
        {
            currentState = AnimFSM.idle;
        }
    }
    public virtual void PickUpAction()
    {

    }
    protected virtual void Death()
    {
        Destroy(this.gameObject);
    }
}
