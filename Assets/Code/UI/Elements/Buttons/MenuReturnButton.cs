using System;
using UnityEngine.EventSystems;

namespace Code.UI.Elements.Buttons
{
  public class MenuReturnButton : Button
  {
    public event Action Clicked;

    public override void OnPointerClick(PointerEventData eventData) =>
      Clicked?.Invoke();
  }
}