using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cherry : Collectible
{
    private BoxCollider2D CherryCollider;
    private Animator CherryAnimator;
    private AudioSource ASource;
    private float HealVal = .05f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        CherryCollider = GetComponent<BoxCollider2D>();
        CherryAnimator = GetComponent<Animator>();
        ASource = GetComponent<AudioSource>();
    }

    public override void PickUpAction()
    {
        ASource.Play();
        UIController.perm.HealthUpdate(HealVal);
        Destroy(this.gameObject, .55f);
    }
}
