using Code.UI.Services.Windows;
using Code.UI.Windows;
using System;

namespace Code.StaticData.Windows
{
  [Serializable]
  public class WindowConfig
  {
    public WindowId WindowId;
    public BaseWindow Prefab;
  }
}