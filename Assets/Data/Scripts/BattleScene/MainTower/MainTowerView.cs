using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

namespace Levels.Tower
{
    public class MainTowerView : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> _renderers;
        private CancellationTokenSource _cts;

        public void SetDamaged(float value)
        {
            _cts = new();
            Light(_cts.Token).Forget();
        }

        private async UniTask Light(CancellationToken token)
        {
            try
            {
                var colorList = new List<Color>();
                foreach (var renderer in _renderers)
                {
                    colorList.Add(renderer.color);
                    renderer.color = Color.red;
                }
                await UniTask.WaitForSeconds(1, false, PlayerLoopTiming.Update, token);

                for (int i = 0; i < colorList.Count; i++)
                {
                    _renderers[i].color = colorList[i];
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Tower Light Stopped");
            }
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
        }

        public void Die()
        {
            foreach (var renderer in _renderers)
            {
                renderer.color = Color.red;

            }
        }
    }
}