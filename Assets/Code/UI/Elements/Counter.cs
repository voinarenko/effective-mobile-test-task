using TMPro;
using UnityEngine;

namespace Code.UI.Elements
{
  public class Counter : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _counter;

    public void UpdateCounter(int currentData) =>
      _counter.text = $"{currentData}";
  }
}