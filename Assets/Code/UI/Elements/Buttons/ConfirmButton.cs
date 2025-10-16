using Code.UI.Windows;
using UnityEngine.EventSystems;

namespace Code.UI.Elements.Buttons
{
  public class ConfirmButton : Button
  {
    private BaseWindow _window;

    protected override void Awake()
    {
      base.Awake();
      _window = GetComponentInParent<BaseWindow>();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
      base.OnPointerClick(eventData);
      Destroy(_window.gameObject);
    }
  }
}