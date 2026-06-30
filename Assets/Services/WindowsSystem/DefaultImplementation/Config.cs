using System.Collections.Generic;
using UnityEngine;

namespace WindowsSystem.DefaultImplementation
{
    [CreateAssetMenu(fileName = DEFAULT_PATH, menuName = "ScriptableObjects/WindowSystem/Config")]
    public class Config : ScriptableObject
    {
        public const string DEFAULT_PATH = "WindowsConfig";
        [SerializeField]
        private AWindow[] _windows;

        public IEnumerable<AWindow> Windows => _windows;
    }
}
