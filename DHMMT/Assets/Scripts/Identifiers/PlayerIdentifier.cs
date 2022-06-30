using Samhereis;
using Samhereis.DI;

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
            DIBox.RemoveSingel<IdentifierBase>(CharacterKeysContainer.mainPlayer);
        }
    }
}