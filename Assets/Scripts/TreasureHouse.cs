using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureHouse : MonoBehaviour
{
    [SerializeField] private string LevelToLoad;
    public void EnterHouse()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
           
}
