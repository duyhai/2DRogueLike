using Godot;

public partial class BindOption : Control
{
    [Export]
    public string action;
    bool waitingForInput = false;
    InputEvent inputEvent;
    Button button;

    public override void _Ready()
    {
        var keys = InputMap.ActionGetEvents(action);
        button = (Button)GetNode("Button");
        button.Pressed += OnButtonPressed;
        inputEvent = keys[0];
        button.Text = keys[0].AsText();
    }

    public void OnButtonPressed()
    {
        waitingForInput = true;
        button.Text = "Waiting for input...";
    }

    public override void _Input(InputEvent @event)
    {
        if (waitingForInput)
        {
            if (@event is InputEventKey eventKey)
            {
                waitingForInput = false;
                button.Text = eventKey.AsText();
                InputMap.ActionEraseEvent(action, inputEvent);
                InputMap.ActionAddEvent(action, eventKey);
                inputEvent = eventKey;
            }
        }
    }
}
