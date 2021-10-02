using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemPickup : Collectible
{
    private BoxCollider2D GemCollider;
    private Animator GemAnimator;
    private AudioSource ASource;
    private int points = 250;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GemCollider = GetComponent<BoxCollider2D>();
        GemAnimator = GetComponent<Animator>();
        ASource = GetComponent<AudioSource>();
    }
    //overriding the parent function
    public override void PickUpAction()
    {
        ASource.Play();
        UIController.perm.ScoreUpdate(points);
    }
}
