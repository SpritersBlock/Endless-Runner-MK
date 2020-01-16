using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject GUINull; //All the coin/score text displayed throughout the game that will be deactivated once the game over panel above displays
    [SerializeField] PlayerJump player;
    [SerializeField] GameObject playerDeathClone; //This will be spawned upon the player's death and fly towards the screen

    [Header("Game Over Text")]
    [SerializeField] TextMeshProUGUI[] gameOverText; //The updated text on the game over screen. 0 = distance, 1 = coins this run, 2 = total coins, 3 = final score

    [Header("Game Over Info")]
    [SerializeField] float timeBetweenDeathAndResults = 1.4f;

    void CheckIfComponentsAreNull()
    {
        if (!gameOverPanel)
        {
            gameOverPanel = transform.GetChild(0).gameObject;
        }
        if (!GUINull)
        {
            GUINull = GameObject.Find("GUI Null");
        }
        if (!player)
        {
            PlayerJump player = FindObjectOfType<PlayerJump>();
        }
    }

    public void UpdateGameOverText(int distance, int locCoin, int globCoin, int score) //Updates the text in the game over screen
    {
        gameOverText[0].text = distance.ToString() + " miles";
        gameOverText[1].text = locCoin.ToString() + " coins";
        gameOverText[2].text = globCoin.ToString() + " total coins";
        gameOverText[3].text = score.ToString();
    }

    public IEnumerator GameOverSequence()
    {
        GameManager.instance.gameActive = false;

        CheckIfComponentsAreNull();

        Instantiate(playerDeathClone, player.transform.position, Quaternion.identity); 
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(timeBetweenDeathAndResults); 
        GUINull.SetActive(false);
        gameOverPanel.SetActive(true);

        yield return null;
    }
    public void RestartGame()
    {
        SceneManagement.instance.GoToScene("runnerScene");
    }
}
