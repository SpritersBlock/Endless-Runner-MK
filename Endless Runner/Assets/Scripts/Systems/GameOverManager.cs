using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    GameObject gameOverPanel;
    CoinManager coinManager;

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel = transform.GetChild(0).gameObject;
        coinManager = FindObjectOfType<CoinManager>();
    }

    public IEnumerator GameOverSequence()
    {
        GameManager.instance.gameActive = false; //Turn the game off so that the environment stops moving and the player stops playing

        PlayerJump player = FindObjectOfType<PlayerJump>(); //Find the player, because we only need it for the death sequence right now

        player.gameObject.SetActive(false); //Deactivate the player object, so we can spawn something else in there instead

        yield return new WaitForSeconds(1f); //Wait for a second before information is given to the player

        coinManager.DeactivateCoinCounterTexts();

        gameOverPanel.SetActive(true);

        yield return null;
    }
    public void RestartGame()
    {
        SceneManagement.instance.GoToScene("runnerScene");
    }
}
