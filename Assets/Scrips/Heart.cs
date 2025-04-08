using UnityEngine;

public class Heart : Collectable
{


    protected override void Start()
    {
        base.Start();
        type = CollectableType.Heart;
    }

    protected override void ApplyEffect(Player player)
    {
        player.Heal(value);
    }


}