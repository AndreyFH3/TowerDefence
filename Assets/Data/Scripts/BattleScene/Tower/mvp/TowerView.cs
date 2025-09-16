using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
namespace Levels.Tower
{
    public class TowerView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _attackStartPoint;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private float _lineRendererShowTime = .25f;

        private void Awake()
        {
            _lineRenderer.gameObject.SetActive(false);
            _lineRenderer.transform.SetParent(null);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
            transform.localScale = Vector3.one / 2;
        }

        public void Attack(Vector3 target)
        {
            _lineRenderer.gameObject.SetActive(true);
            _lineRenderer.SetPositions(new[] { _attackStartPoint.position , target});
            DisableLineRenderer(new CancellationTokenSource().Token).Forget();
        }
        private async UniTask DisableLineRenderer(CancellationToken token)
        {
            await UniTask.WaitForSeconds(_lineRendererShowTime);
            _lineRenderer.gameObject.SetActive(false);
        }

        public void SetLevel(int level) => _levelText.text = $"{level}";

        public void UpdateSpriteRenderer(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }


    }
}