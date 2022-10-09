using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Composites;

public class AnimationHelper : MonoBehaviour
{
    public UnityEvent OnAnimationEventTriggered;

    public void TriggerEvent()
    {
        OnAnimationEventTriggered?.Invoke();
    }
}
