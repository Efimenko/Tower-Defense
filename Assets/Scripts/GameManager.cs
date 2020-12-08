using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;
    public GameObject gameOverUI;

    private void Start()
    {
        gameIsOver = false;
    }

    private void Update()
    {
        if (gameIsOver)
        {
            return;
        }

        // TODO: remove after testing
        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }

        if (PlayerStats.instance.lives <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
