namespace Entitas
{

    /// Implement this interface if you want to create a system which should
    /// restart down once in the end.
    public interface IRestartSystem : ISystem
    {

        void Restart();
    }
}
