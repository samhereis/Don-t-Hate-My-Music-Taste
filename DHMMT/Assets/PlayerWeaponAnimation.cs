using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAnimation : MonoBehaviour
{
    PlayerMove playerMove;
    string SPEED = "Speed";
    float speed ;
    private void OnEnable()
    {
        ExtentionMethods.SetWithNullCheck(ref playerMove, GetComponent<PlayerMove>());

        PlayerInput.input.Gameplay.Move.performed += context => { speed = playerMove.speedMultiplier; SetSpeed(speed); };
        PlayerInput.input.Gameplay.Move.canceled += context => { speed = 0; SetSpeed(speed); };

        PlayerInput.input.Gameplay.Sprint.performed += context => SetSpeed(speed * playerMove.speedMultiplier);
        PlayerInput.input.Gameplay.Sprint.canceled += context => SetSpeed(speed = speed = playerMove.speedMultiplier);

        PlayerInput.input.Gameplay.Fire.performed += context => SetSpeed(speed);
    }
    void SetSpeed(float value)
    {
        PlayerWeaponDataHolder.instance.gunData?.animator.SetFloat(SPEED, value);
    }
}
