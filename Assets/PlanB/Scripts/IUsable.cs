namespace PlanB.Assets
{
    public interface IUsable
    {
        bool Active { get;  }
        bool Changable { get; }
        string ActionName { get; }
        void Use();
        public void Next();
    }
}
