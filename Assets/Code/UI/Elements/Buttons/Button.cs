using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.Elements.Buttons
{
  public class Button : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler,
    IPointerEnterHandler, IPointerExitHandler
  {
    public event Action Clicked;
    
    private Image _image;

    [SerializeField] private Sprite _normal;
    [SerializeField] private Sprite _pressed;
    [SerializeField] private Sprite _hover;

    protected void Awake() =>
      TryGetComponent(out _image);

    public void OnPointerClick(PointerEventData eventData) =>
      Clicked?.Invoke();

    public void OnPointerDown(PointerEventData eventData) =>
      _image.sprite = _pressed;

    public void OnPointerUp(PointerEventData eventData) =>
      _image.sprite = _normal;

    public void OnPointerEnter(PointerEventData eventData) =>
      _image.sprite = _hover;

    public void OnPointerExit(PointerEventData eventData) =>
      _image.sprite = _normal;
  }
}