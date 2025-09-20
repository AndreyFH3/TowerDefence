using System.Collections.Generic;
using Core;
using UnityEngine;

namespace PlayerData
{
    [System.Serializable]
    public class CompanyProgress
    {
        [SerializeField] private List<string> _passedIds = new();

        public System.Action OnPassed;
        public int LastPassed => _passedIds.Count;

        public void SetPassed(string id) 
        {
            if (!CheckPassed(id))
            {
                _passedIds.Add(id);
                new SaveSystem().Save(this, "COMPANY_PROGRESS");
            }
        }

        public bool CheckPassed(string id)
        {
            return _passedIds.Contains(id);
        }
    }
}