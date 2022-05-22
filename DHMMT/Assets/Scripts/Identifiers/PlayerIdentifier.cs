namespace Identifiers
{
    public class PlayerIdentifier : IdentifierBase
    {
        public static PlayerIdentifier instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }
    }
}