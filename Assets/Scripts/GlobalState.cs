abstract public class GlobalState<T>: State<T>
{

    override abstract public void Enter(T agent);
    override abstract public void Execute(T agent);
    override abstract public void Exit(T agent);
}
