﻿using Code.Data;

namespace Code.Services.Progress
{
  public interface ISavedProgressReader
  {
    void LoadProgress(PlayerProgress progress);
  }

  public interface ISavedProgress : ISavedProgressReader
  {
    void UpdateProgress(PlayerProgress progress);
  }
}