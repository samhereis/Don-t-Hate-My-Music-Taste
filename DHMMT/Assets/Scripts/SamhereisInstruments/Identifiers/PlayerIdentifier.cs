using DI;

namespace Identifiers
{
    public class PlayerIdentifier : IdentifierBase
    {
        private void Awake()
        {
            DIBox.RegisterSingle<IdentifierBase>(this, CharacterKeysContainer.mainPlayer);
        }

        private void OnDestroy()
        {
            DIBox.RemoveSingle<IdentifierBase>(CharacterKeysContainer.mainPlayer);
        }
    }
}