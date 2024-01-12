namespace DataClasses
{
    public struct PlayerHealthData
    {
        public float healthBefore;
        public float healthAfter;
        public float maxHealth;

        public PlayerHealthData(float healthBefore, float healthAfter, float maxHealth)
        {
            this.healthBefore = healthBefore;
            this.healthAfter = healthAfter;
            this.maxHealth = maxHealth;
        }
    }
}