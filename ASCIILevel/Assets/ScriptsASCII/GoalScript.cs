using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    //On collision with the player make the next level

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Increase the currentLevel property
        GameManager.Instance.GetComponent<ASCIILevelLoader>().CurrentLevel++;
    }
}
