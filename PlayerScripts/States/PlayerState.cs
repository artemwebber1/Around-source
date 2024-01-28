using Assets.Scripts.StateManagement;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts.States
{
    public abstract class PlayerState : State
    {
        protected readonly Player player;

        public PlayerState(Player player)
            => this.player = player;
    }
}
