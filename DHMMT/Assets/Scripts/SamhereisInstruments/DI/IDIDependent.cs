using System.Threading.Tasks;

namespace Samhereis.DI
{
    public interface IDIDependent
    {
        public async Task LoadDependencies()
        {
            await DIBox.InjectDataToClass(this);
        }
    }
}