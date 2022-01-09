public static class PlayerInput
{
    // Player's input data manager

    public enum InputState { Gameplay, UI }
    public static InputState State;
    private readonly static InputSettings _playersInputState = new InputSettings();
    public static InputSettings playersInputState => _playersInputState;

    public static void SetInput(InputState setTo)
    {
        Enable();

        if (setTo == State) return;

        if (setTo == InputState.Gameplay)
        {
            _playersInputState.Gameplay.Enable();
            _playersInputState.UI.Disable();
        }
        else
        {
            _playersInputState.Gameplay.Disable();
            _playersInputState.UI.Enable();
        }

        State = setTo;
    }

    public static void Enable()
    {
        _playersInputState.Enable();
    }

    public static void Disable()
    {
        _playersInputState.Disable();
    }
}
