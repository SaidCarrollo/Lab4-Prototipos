using UnityEngine;

public class Coin : Collectable
{


    protected override void Start()
    {
        base.Start();
        type = CollectableType.Coin;
    }

    protected override void ApplyEffect(Player player)
    {
        player.AddScore(value);
    }


}