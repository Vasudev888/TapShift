using UnityEngine;

public class ScorePickup : PickUp
{
    public int multiplier;
    public float duration;

    #region Override Functions
    protected override void OnPlayerCollect()
    {
        base.OnPlayerCollect();
        // specific logic
        game.HandleScorePickup(multiplier, duration);
    }
    #endregion
}
