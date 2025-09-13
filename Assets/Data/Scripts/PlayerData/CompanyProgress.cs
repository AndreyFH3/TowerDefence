using UnityEngine;

namespace PlayerData
{
    [System.Serializable]
    public class CompanyProgress
    {
        [SerializeField] private int _lastPassed = 0;

        public System.Action OnPassed;
        public int LastPassed => _lastPassed;

        public void SetPassedLevel()
        {
            _lastPassed++;
            OnPassed?.Invoke();
        }

        public bool CheckPassedLevel(int value) => _lastPassed <= value;
    }
}