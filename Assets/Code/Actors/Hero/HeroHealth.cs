using Code.Data;
using System;
using UnityEngine;

namespace Code.Actors.Hero
{
  public class HeroHealth : MonoBehaviour, IHealth
  {
    public float Max
    {
      get => _progress.MaxHealth;
      set => _progress.MaxHealth = value;
    }

    public float Current
    {
      get => _progress.CurrentHealth;
      set => _progress.CurrentHealth = value;
    }

    private PlayerProgress _progress;

    public void Construct(PlayerProgress progress) =>
      _progress = progress;

    public void TakeDamage(float damage)
    {
      if (Current <= 0) return;
      Current -= damage;
    }
  }
}