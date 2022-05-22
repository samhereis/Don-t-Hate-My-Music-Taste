using Identifiers;

namespace Interfaces
{
    public interface IInteractable
    {
        public bool isInteractable { get; }
        public string ItemName { get; }
        public void Interact(IdentifierBase caller);
    }
}