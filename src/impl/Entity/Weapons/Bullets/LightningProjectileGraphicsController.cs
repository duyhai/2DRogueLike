using Godot;
using Godot.Collections;

public class LightningProjectileGraphicsController : GraphicsController
{
    PackedScene LightningScene = (PackedScene)GD.Load("res://Lightning.tscn");

    public override void Update(GameObject gameObject)
    {

    }

    public void ChainingBodiesAnimation(GameObject gameObject, Array bodiesHit)
    {
        if (bodiesHit.Count != 0)
        {
            ConnectLightning(gameObject, gameObject, (GameObject)bodiesHit[0]);
        }

        for (int i = 0; i < bodiesHit.Count - 1; i++)
        {
            ConnectLightning(gameObject, (GameObject)bodiesHit[i], (GameObject)bodiesHit[i + 1]);
        }
    }

    private void ConnectLightning(GameObject gameObject, GameObject o1, GameObject o2)
    {
        var lightning = (Lightning)LightningScene.Instance();
        lightning.AddPoint(o1.GlobalPosition);
        lightning.AddPoint(o2.GlobalPosition);
        gameObject.GetParent().AddChild(lightning);
        lightning.PlayAnimation();
    }
}