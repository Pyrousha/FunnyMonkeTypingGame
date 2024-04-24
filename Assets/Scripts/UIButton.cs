using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIButton : Button, ISelectHandler, IDeselectHandler
{
    protected List<Graphic> graphics = new List<Graphic>();

    protected override void Awake()
    {
        graphics = new List<Graphic>(GetComponentsInChildren<Graphic>());
    }

    //protected override void Start()
    //{
    //    base.Start();
    //    normalColors = button.colors;

    //    disabledColors = button.colors;
    //    disabledColors.normalColor *= 0.5f;
    //    disabledColors.selectedColor *= 0.5f;
    //    disabledColors.pressedColor *= 0.5f;
    //}

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        var targetColor =
            state == SelectionState.Disabled ? colors.disabledColor :
            state == SelectionState.Highlighted ? colors.highlightedColor :
            state == SelectionState.Normal ? colors.normalColor :
            state == SelectionState.Pressed ? colors.pressedColor :
            state == SelectionState.Selected ? colors.selectedColor : Color.white;

        foreach (Graphic graphic in graphics)
        {
            graphic.CrossFadeColor(targetColor, instant ? 0f : colors.fadeDuration, true, true);
        }
    }
}