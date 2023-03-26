using System;
using Godot;

public partial class PlayerFollowingInputController : InputController
{
    public override void Update(GameObject gameObject)
    {
        if (gameObject.isDead) return;

        gameObject.velocity = Vector2.Zero;

        if (!gameObject.IsInGroup(NodeGroups.Enemy)) return;

        Enemy enemy = (Enemy)gameObject;
        Player player = enemy.SightCheck();
        if (player != null)
        {
            gameObject.velocity = (player.Position - gameObject.Position).Normalized() * gameObject.Stats.Speed;
        }
    }
}