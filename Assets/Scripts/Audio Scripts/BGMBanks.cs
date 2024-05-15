using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BGMBanks", menuName = "Custom/BGMBanks")]
public class BGMBanks : ScriptableObject
{
    [System.Serializable]
    public class BGMS
    {
        [HideInInspector]
        public string name = "BGM Info";
        public AudioClip Track;
        public long loopOffset;
        public bool isLooping;

        // Expose the name with index in the Inspector
        public string NameWithIndex
        {
            get { return name; }
        }
    }

    public List<BGMS> BGM = new List<BGMS>();
}
