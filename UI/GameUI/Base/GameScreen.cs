using UnityEngine;


namespace Assets.Scripts.UI.Screens.Base
{
    public abstract class GameScreen : MonoBehaviour
    {
        protected PlayerUIinitializer Canvas;

        public virtual void Init() => Canvas = GetComponentInParent<PlayerUIinitializer>();
    }
}
