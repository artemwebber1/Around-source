using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Pool
{
    public abstract class PoolObject : MonoBehaviour
    {
        public void ReturnToPool()
            => gameObject.SetActive(false);
    }
}

