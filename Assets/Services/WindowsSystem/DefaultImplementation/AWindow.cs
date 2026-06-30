using Cysharp.Threading.Tasks;
using UnityEngine;

namespace WindowsSystem.DefaultImplementation
{
    public abstract class AWindow : MonoBehaviour, IWindow
    {
        [SerializeField] private Canvas _canvas;


        protected virtual void Awake()
        {
            _canvas.enabled = false;
        }


        internal virtual UniTask Open()
        {
            _canvas.enabled = true;
            return UniTask.CompletedTask;
        }

        internal virtual UniTask Close()
        {
            _canvas.enabled=false;
            return UniTask.CompletedTask;
        }
    }
}
