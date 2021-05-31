using UnityEngine;
using UnityCore.Menu;
using UnityEngine.UI;

public class GameOverPage : Page
{
    public PageController pages;
    public Text scoreText;

    #region Public Functions
    public void TryAgain()
    {
        // tell the game to reset
        GameController.instance.TryAgain();
        // close the menu page
        pages.TurnPageOff(type);
    }

    public void GoToHome()
    {
        //turn off this page
        pages.TurnPageOff(type);

        //turn on the main menu page
        pages.TurnPageOn(PageType.Menu);
    }
    #endregion

    #region Override Functions
    protected override void OnPageEnabled()
    {
        // capture the player score and display it
        scoreText.text = "Player Score: " + GameController.instance.score.ToString();
        // store a referenceto the game
    }
    #endregion
}
