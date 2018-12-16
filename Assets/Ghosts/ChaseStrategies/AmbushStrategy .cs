﻿using RoyT.AStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Ghosts.ChaseStrategies
{
    public class AmbushStrategy : IChaseStrategy
    {
        private Vector2[] positions;
        private int currentPos = 0;
        private Vector2Int goal;
        private Ghost blinky;

        private Vector2Int lastTilePosition;

        public void Start(Ghost ghost, Ghost blinky)
        {
            this.blinky = blinky;
            Debug.Log(ghost.name + " ambushing");
            ambushPlayer(ghost);
        }

        public bool Chase(Ghost ghost)
        {
            if (positions != null)
            {
                ghost.target = positions[currentPos];
                if (ghost.body.position == ghost.target)
                {
                    var ghostTilePosition = Vector2Int.FloorToInt(ghost.body.position / 16);
                    if (lastTilePosition != ghostTilePosition && GameManager.isEdgeTile(ghostTilePosition))
                    {
                        ambushPlayer(ghost);
                        lastTilePosition = ghostTilePosition;
                    }
                    else
                    {
                        if (currentPos < positions.Length - 1)
                        {
                            currentPos++;
                        }
                        else
                        {
                            if (Vector2Int.FloorToInt(ghost.body.position / 16) != goal)
                            {
                                ambushPlayer(ghost);
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void ambushPlayer(Ghost ghost)
        {
            int lookAhead = 4;
            currentPos = 0;
            do
            {
                var start = Vector2Int.FloorToInt(ghost.body.position / 16);
                goal = Vector2Int.FloorToInt(ghost.playerBody.position / 16 + ghost.player.direction * lookAhead);
                positions = GameManager.GetPath(start, goal);
                lookAhead -= 1;
            }
            while (positions == null || positions.Count() == 0);
        }
    }
}
