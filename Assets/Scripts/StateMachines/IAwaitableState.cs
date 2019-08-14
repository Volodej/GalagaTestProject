using System.Threading.Tasks;

namespace StateMachines
{
    public interface IAwaitableState<out TStateType>
    {
        Task RunState();
        TStateType Type { get; }
        bool IsExitState { get; }
    }
}