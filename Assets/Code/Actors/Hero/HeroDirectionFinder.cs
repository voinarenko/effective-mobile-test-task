using UnityEngine;

namespace Code.Actors.Hero
{
  public class HeroDirectionFinder : MonoBehaviour
  {
    private const int DefaultDirection = 99;
    private const int TopDirection = 0;
    private const int BottomDirection = 1;
    private const int RightDirection = 2;
    private const int LeftDirection = 3;
    [SerializeField] private Animator _animator;
    [SerializeField] private HeroAnimate _heroAnimate;
    private float _horizontal;
    private float _vertical;

    public int GetDirection()
    {
      var result = DefaultDirection;
      _horizontal = _animator.GetFloat(_heroAnimate.AnimIdHorizontal);
      _vertical = _animator.GetFloat(_heroAnimate.AnimIdVertical);

      result = _vertical switch
      {
        > 0 when _vertical > Mathf.Abs(_horizontal) => TopDirection,
        < 0 when Mathf.Abs(_vertical) > Mathf.Abs(_horizontal) => BottomDirection,
        _ => _horizontal switch
        {
          > 0 when _horizontal > Mathf.Abs(_vertical) => RightDirection,
          < 0 when Mathf.Abs(_horizontal) > Mathf.Abs(_vertical) => LeftDirection,
          _ => result
        }
      };

      return result;
    }
  }
}