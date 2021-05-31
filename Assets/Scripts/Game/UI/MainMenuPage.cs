using UnityEngine;
using UnityCore.Menu;
using UnityEngine.UI;
using UnityCore.Audio;

public class MainMenuPage : Page
{

    public PageController pages;

    #region Public Functions
    public void StartGame()
    {
        // tell the game to play
        GameController.instance.TryAgain();
        // close the menu page
        pages.TurnPageOff(type);
    }
    #endregion

    #region Override Functions
    protected override void OnPageEnabled()
    {
        AudioController.instance.PlayAudio(UnityCore.Audio.AudioType.ST_01, true, 1);
    }
    #endregion
}
