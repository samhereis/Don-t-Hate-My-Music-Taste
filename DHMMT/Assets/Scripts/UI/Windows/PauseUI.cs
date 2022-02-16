using Scriptables;
using Sripts;
using UnityEngine;

public class PauseUI : UIWIndowBase
{
    [SerializeField] private Input_SO _input;

    private void Awake()
    {
        if (!_input)
        {
            AddressableGetter.GetAddressable<Input_SO>(nameof(Input_SO), (result) =>
            {
                _input = result;
            });
        }
    }

    protected override void Disable()
    {
        
    }
    protected override void Enable()
    {

    }
    public override void Setup()
    {
        
    }
}
