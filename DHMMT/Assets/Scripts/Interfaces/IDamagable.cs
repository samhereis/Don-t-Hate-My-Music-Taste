using Photon.Pun;

namespace Interfaces
{
    public interface IDamagable
    {
        public void TakeDamage(float damage);
        public void RPC_TakeDamage(float damage);
    }
}