using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
namespace Levels.Tower
{
    public class TowerView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _attackStartPoint;
        [SerializeField] private float _lineRendererShowTime = .25f;

        private void Awake()
        {
            _lineRenderer.gameObject.SetActive(false);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Attack(Vector3 target)
        {
            _lineRenderer.gameObject.SetActive(true);
            _lineRenderer.SetPosition(0, _attackStartPoint.position);
            _lineRenderer.SetPosition(1, target);
            DisableLineRenderer(new CancellationTokenSource().Token).Forget();
        }
        private async UniTask DisableLineRenderer(CancellationToken token)
        {
            await UniTask.WaitForSeconds(_lineRendererShowTime);
            _lineRenderer.gameObject.SetActive(false);
        }

        public void UpdateSpriteRenderer(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}