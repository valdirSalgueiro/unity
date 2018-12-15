﻿using Assets.Ghosts;
using Assets.Ghosts.State;
using RoyT.AStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class Ghost : MonoBehaviour
    {
        public GameObject player;
        public Vector2 target;        
        public Rigidbody2D body;
        public Rigidbody2D playerBody;

        private Animator animator;

        protected GhostState state;

        // Use this for initialization
        void Start()
        {
            body = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            playerBody = player.GetComponent<Rigidbody2D>();

            state = new ChaseState();
        }

        void FixedUpdate()
        {
            state.update(this);

            if (state == typeof(ChaseState))
            {
                var direction = (target - body.position).normalized;
                if (direction == Vector2Int.right)
                {
                    animator.SetTrigger("right");
                }
                else if (direction == Vector2Int.down)
                {
                    animator.SetTrigger("down");
                }
                else if (direction == Vector2Int.up)
                {
                    animator.SetTrigger("up");
                }
                else
                {
                    animator.SetTrigger("left");
                }
            }
        }
    }
}
