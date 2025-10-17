using Code.UI.Windows;
using UnityEngine;

namespace Code.UI.Services.Factory
{
  public class UiFactory : IUiFactory
  {
    public EndGameWindow EndGameWindow { get; set; }
    public Transform UIRoot { get; set; }
  }
}