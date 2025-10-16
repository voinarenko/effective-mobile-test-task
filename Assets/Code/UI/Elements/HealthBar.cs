using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Elements
{
  public class HealthBar : MonoBehaviour
  {
    [SerializeField] private Image _imageCurrent;

    public void SetValue(float current, float max) =>
      _imageCurrent.fillAmount = current / max;
  }
}