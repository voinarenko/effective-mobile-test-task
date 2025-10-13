using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Code.Services.Async
{
  public class AsyncService : IAsyncService
  {
    private readonly CancellationTokenSource _cts = new();

    public AsyncService() =>
      Application.quitting += OnApplicationQuitting;

    public async UniTask NextFrame(CancellationTokenSource cts = null)
    {
      if (cts != null) await UniTask.NextFrame(cts.Token);
      else await UniTask.NextFrame(_cts.Token);
    }

    public async UniTask WaitForSeconds(float duration, CancellationTokenSource cts = null)
    {
      if (cts != null) await UniTask.WaitForSeconds(duration, cancellationToken: cts.Token);
      else await UniTask.WaitForSeconds(duration, cancellationToken: _cts.Token);
    }

    public async UniTask WaitWithPause(float durationSeconds, Func<bool> isPaused)
    {
      var elapsed = 0f;
      while (elapsed < durationSeconds)
      {
        await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: _cts.Token);
        if (!isPaused()) elapsed += Time.deltaTime;
      }
    }

    private void OnApplicationQuitting()
    {
      Application.quitting -= OnApplicationQuitting;
      _cts?.Cancel();
      _cts?.Dispose();
    }
  }
}