using Levels.Info;
using Menu.LevelSelect;
using UnityEngine;
using UnityEngine.UI;

namespace Levels.Enemies
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Slider _healthLine;
        [SerializeField] private Animator _animator;

        public System.Action OnDestroyGameObject;

        public void SetWalkingAnimation()
        {
            _animator.SetTrigger("WALK");
        }

        public void SetDieAnimation()
        {
            _animator.SetTrigger("DIE");
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            OnDestroyGameObject?.Invoke();
        }


        public class Factory : Zenject.PlaceholderFactory<EnemyView> { }
    }
}