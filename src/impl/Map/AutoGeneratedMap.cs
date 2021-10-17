using Godot;

public class AutoGeneratedMap : Map
{
    private Generator generator;

    public AutoGeneratedMap(int width, int height, int unit, Node parentNode) :
        base(width, height, unit, parentNode)
    {
        // this.generator = new GeneratorV1(this, 10, 5, 10);
        this.generator = new CaveGenerator(this);
    }

    override public void Instance()
    {
        this.generator.GenerateMap();
        base.Instance();
    }
}
