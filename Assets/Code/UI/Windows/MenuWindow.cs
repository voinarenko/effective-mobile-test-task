using UnityEngine;
using Zenject;

namespace Code.UI.Windows
{
  public class MenuWindow : MonoBehaviour
  {
    [Inject]
    public void Construct()
    {
      
    }

    private void Start() =>
      Cursor.visible = true;

    public void Init()
    {
    }
  }
}