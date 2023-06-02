using GameCore.GameManager;
using GameCore.Patterns;

namespace GameCore
{
    public abstract class BaseGameManagerModuleController<TData> : DataControllerBase<TData>, IBaseGameManagerModuleController
        where TData : BaseGameManagerModule
    {
        protected GameManager.GameManager GameManager { get; private set; }

        public void GameManagerPrepared(GameManager.GameManager gameManager)
        {
            GameManager = gameManager;
            GameManagerPrepared();
        }

        protected virtual void GameManagerPrepared()
        {

        }
    }

    public interface IBaseGameManagerModuleController : IController
    {
        public void GameManagerPrepared(GameManager.GameManager gameManager);
    }
}
