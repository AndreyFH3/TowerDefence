using UnityEngine;
namespace Core
{
    public class SaveSystem 
    {
        public T Get<T>(string id) where T : class
        {
            if (!PlayerPrefs.HasKey(id))
                return null;
            
            var data = PlayerPrefs.GetString(id);
            return JsonUtility.FromJson<T>(data);
        }

        public void Save(object obj, string key)
        {
            var data = JsonUtility.ToJson(obj);
            PlayerPrefs.SetString(key, data);
            PlayerPrefs.Save();
        }
    }
}
