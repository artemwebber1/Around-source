using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.MainMenuEntities
{
    public sealed class MainMenuBackground : MonoBehaviour
    {
        [SerializeField] private float _distance;
        [SerializeField] private float _speedMove;

        private Vector3 _moveDirection;

        private float targetPointRight;
        private float targetPointLeft;

        private void Awake()
        {
            targetPointRight = transform.position.x + _distance;
            targetPointLeft = transform.position.x - _distance;

            _moveDirection = Vector3.left;
        }

        private void Update()
        {
            transform.position += _moveDirection * _speedMove * Time.deltaTime;

            if (transform.position.x > targetPointRight)
                _moveDirection = Vector3.left;
            else if (transform.position.x < targetPointLeft)
                _moveDirection = Vector3.right;
        }
    }
}