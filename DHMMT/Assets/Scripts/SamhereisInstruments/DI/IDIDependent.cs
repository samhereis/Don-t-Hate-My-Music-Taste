namespace DI
{
    public interface IDIDependent
    {
        public async void LoadDependencies()
        {
            await DIBox.InjectDataTo(this);
        }
    }
}