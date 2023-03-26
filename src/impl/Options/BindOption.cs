using Godot;

public partial class BindOption : Container
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
        button.Connect("pressed",new Callable(this,nameof(OnButtonPressed)));
        inputEvent = (InputEvent)keys[0];
        if (keys[0] is InputEventKey eventKey)
        {
            button.Text = OS.GetKeycodeString(eventKey.Keycode);
        }
    }

    public void OnButtonPressed()
    {
        waitingForInput = true;
        button.Text = "Waiting for input...";
    }
    
    public override void _Input(InputEvent @event)
    {
        if (waitingForInput == true)
        {
            if (@event is InputEventKey eventKey)
            {
                waitingForInput = false;
                button.Text = OS.GetKeycodeString(eventKey.Keycode);
                InputMap.ActionEraseEvent(action, inputEvent);
                InputMap.ActionAddEvent(action, eventKey);
            }
        }
    }
}
