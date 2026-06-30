using System;
using System.Threading.Tasks;

namespace WindowsSystem
{
    public interface IWindowsSystem
    {
        ValueTask  Open<T>() where T : IWindow;
        ValueTask Close<T>() where T : IWindow;

        T Get<T>() where T : IWindow;


        ValueTask Open(Type t);
        ValueTask Close(Type t);

        IWindow Get(Type t);
    }
}
