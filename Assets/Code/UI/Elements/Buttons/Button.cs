using Code.Infrastructure.States.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.Elements.Buttons
{
  public abstract class Button : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler,
    IPointerEnterHandler, IPointerExitHandler
  {
    protected IGameStateMachine StateMachine;
    private Image _image;

    [SerializeField] private Sprite _normal;
    [SerializeField] private Sprite _pressed;
    [SerializeField] private Sprite _hover;

    public void Construct(IGameStateMachine stateMachine) =>
      StateMachine = stateMachine;

    protected virtual void Awake() =>
      TryGetComponent(out _image);

    public void OnPointerDown(PointerEventData eventData) =>
      _image.sprite = _pressed;

    public void OnPointerUp(PointerEventData eventData) =>
      _image.sprite = _normal;

    public void OnPointerEnter(PointerEventData eventData) =>
      _image.sprite = _hover;

    public void OnPointerExit(PointerEventData eventData) =>
      _image.sprite = _normal;

    public virtual void OnPointerClick(PointerEventData eventData)
    {

    }
  }
}