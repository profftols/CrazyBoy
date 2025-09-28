namespace _ProjectBoy.Scripts.UI
{
    public class Presenter<T> where T : IView
    {
        protected Presenter(T view)
        {
            View = view;
        }

        protected T View { get; }
    }
}