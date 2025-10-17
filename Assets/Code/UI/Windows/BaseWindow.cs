using UnityEngine;

namespace Code.UI.Windows
{
  public abstract class BaseWindow : MonoBehaviour
  {
    protected virtual void Start()
    {
      Cursor.visible = true;
      Initialize();
      SubscribeUpdates();
    }

    protected virtual void OnDestroy() =>
      Cleanup();

    public virtual void Init()
    {
    }

    protected virtual void Initialize()
    {
    }

    protected virtual void SubscribeUpdates()
    {
    }

    protected virtual void Cleanup()
    {
    }
  }
}