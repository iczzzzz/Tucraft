using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
	
    const float MAX_LIVES = 10f;
	
    float lives;
	
    private void Start()
    {
        lives = MAX_LIVES;
        HeartsManager.instance.SetNHearts(lives, false);
    }

    public void IncreaseLives(float kal)
    {
        float prevLives = lives;
        lives = Mathf.Clamp(lives + RoundPointFive(kal), 0f, MAX_LIVES);
        if (prevLives < MAX_LIVES) {
            HeartsManager.instance.SetNHearts(lives, true);
        }
        //Debug.Log("Lives: " + lives);
    }

    public void DecreaseLives(float damage)
    {
        float prevLives = lives;
        lives = Mathf.Clamp(lives - RoundPointFive(damage), 0f, MAX_LIVES);
        if (prevLives > 0f) {
            HeartsManager.instance.SetNHearts(lives, true);
            if (lives <= 0) {
                Die();
            }
        }
        //Debug.Log("Lives: " + lives);
    }

    void Die()
    {
        Debug.Log("Dead");
    }
	
    float RoundPointFive(float n)
    {
        return ((float)Mathf.Round(n * 2)) / 2.0f;
    }
}
