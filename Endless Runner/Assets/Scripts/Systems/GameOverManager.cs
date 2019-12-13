using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("Game Objects")]
    GameObject gameOverPanel;
    GameObject GUINull;

    [Header("Game Over Text")]
    public TextMeshProUGUI[] gameOverText; //The updated text on the game over screen. 0 = distance, 1 = coins this run, 2 = total coins, 3 = final score

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel = transform.GetChild(0).gameObject;
        GUINull = GameObject.Find("GUI Null");
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
        GameManager.instance.gameActive = false; //Turn the game off so that the environment stops moving and the player stops playing

        PlayerJump player = FindObjectOfType<PlayerJump>(); //Find the player, because we only need it for the death sequence right now

        player.gameObject.SetActive(false); //Deactivate the player object, so we can spawn something else in there instead

        yield return new WaitForSeconds(1f); //Wait for a second before information is given to the player

        GUINull.SetActive(false); //Keep all the GUI elements as children of a null to deactivate them all at once

        gameOverPanel.SetActive(true);

        yield return null;
    }
    public void RestartGame()
    {
        SceneManagement.instance.GoToScene("runnerScene");
    }
}
