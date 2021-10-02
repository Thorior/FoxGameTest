using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController perm;

   [SerializeField]public Image Health;
   [SerializeField]private Text Score;

    public int currentScore = 0;
    public float currentHealth = 1f;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //singleton
        if (!perm)
        {
            perm = this;
        }
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ScoreUpdate(int points) 
    {
        currentScore = currentScore + points;
        Score.text = currentScore.ToString();
    }

    public void HealthUpdate(float HP)
    {
        Health.fillAmount = currentHealth + HP;
    }
}
