using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Enemy : MonoBehaviour {

	public HexTile tile;
	public Stats enemy_stats;

	public int Status;
	public float movement;
	public int special;
	public List<float> att_range;
	public List<Unit> attackablePlayers;
	private Unit attackablePlayer;

    //PriorityQueue<float, SearchNode<HexTile, Action>> frontier = new PriorityQueue<float, SearchNode<HexTile, Action>>();
    //List<SearchNode<HexTile, Action>> enemyList = new List<SearchNode<HexTile, Action>>();
    //Dictionary<HexTile, SearchNode<HexTile, Action>> visitedTiles = new Dictionary<HexTile, SearchNode<HexTile, Action>>();

	void Start() {
		enemy_stats = this.GetComponentInChildren<Stats> ();
		Status = 0;
	}

	public void InitPosition () {
		this.transform.position = tile.transform.position;
		this.transform.Translate (new Vector3 (0.0f, 0.5f, 0.0f));
		movement = this.GetComponentInChildren<Stats> ().Mov;
	}

	public void MoveEnemy() {
		attackablePlayer = null;

		FindPossiblePlayers ();
		Debug.Log (attackablePlayers);

		for (int i = 0; i < attackablePlayers.Count; i++) {
			if (i == 0 || attackablePlayer.char_stats.cHP >= attackablePlayers [i].char_stats.cHP) {
				attackablePlayer = attackablePlayers [i];
			}
		}

		if (attackablePlayer != null) {
			attackablePlayers = new List<Unit> ();
			FindAttackablePlayers (this.tile);

			if (attackablePlayers.Contains (attackablePlayer)) {
				GameManager.instance.InitiateBattle (this.tile, attackablePlayer.tile);
				Status = 0;
			} else {
				MoveTowardsPlayer (attackablePlayer);
				Status = 2;
			}
		} else {
			Status = 0;
		}

        /*if (movement == 0)
        {
            Active = false;
        }
        else
        {
            CalculateAvailableMovementTiles();
            MoveToEnemy();
        }*/

	}

	private void FindPossiblePlayers() {
		List<Unit> attackablePlayers = new List<Unit>();

		GameManager.instance.ResetTileMovDis ();
		float totalMov = movement;
		HexTile viewTile = null;
		List<HexTile> visitedTile = new List<HexTile> ();

		this.tile.mov_dis = 0;

		visitedTile.Add (this.tile);
		viewTile = this.tile;

		while (visitedTile.Count > 0) {
			foreach (HexTile tile in visitedTile) {
				if (viewTile == null || tile.mov_dis < viewTile.mov_dis) {
					viewTile = tile;
				}
			}

			visitedTile.Remove (viewTile);
			FindAttackablePlayers (viewTile);

			if (viewTile.E_Tile != null && viewTile.E_Tile.character == null && viewTile.E_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.E_Tile.mov_dis != -1 && viewTile.E_Tile.mov_dis > viewTile.mov_dis + viewTile.E_Tile.mov_cost) {
					viewTile.E_Tile.mov_dis = viewTile.mov_dis + viewTile.E_Tile.mov_cost;
				} else if (viewTile.E_Tile.mov_dis == -1) {
					viewTile.E_Tile.mov_dis = viewTile.mov_dis + viewTile.E_Tile.mov_cost;
					visitedTile.Add (viewTile.E_Tile);
				}
			}

			if (viewTile.W_Tile != null && viewTile.W_Tile.character == null && viewTile.W_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.W_Tile.mov_dis != -1 && viewTile.W_Tile.mov_dis > viewTile.mov_dis + viewTile.W_Tile.mov_cost) {
					viewTile.W_Tile.mov_dis = viewTile.mov_dis + viewTile.W_Tile.mov_cost;
				} else if (viewTile.W_Tile.mov_dis == -1) {
					viewTile.W_Tile.mov_dis = viewTile.mov_dis + viewTile.W_Tile.mov_cost;
					visitedTile.Add (viewTile.W_Tile);
				}
			}

			if (viewTile.SE_Tile != null && viewTile.SE_Tile.character == null && viewTile.SE_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.SE_Tile.mov_dis != -1 && viewTile.SE_Tile.mov_dis > viewTile.mov_dis + viewTile.SE_Tile.mov_cost) {
					viewTile.SE_Tile.mov_dis = viewTile.mov_dis + viewTile.SE_Tile.mov_cost;
				} else if (viewTile.SE_Tile.mov_dis == -1) {
					viewTile.SE_Tile.mov_dis = viewTile.mov_dis + viewTile.SE_Tile.mov_cost;
					visitedTile.Add (viewTile.SE_Tile);
				}
			}

			if (viewTile.SW_Tile != null && viewTile.SW_Tile.character == null && viewTile.SW_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.SW_Tile.mov_dis != -1 && viewTile.SW_Tile.mov_dis > viewTile.mov_dis + viewTile.SW_Tile.mov_cost) {
					viewTile.SW_Tile.mov_dis = viewTile.mov_dis + viewTile.SW_Tile.mov_cost;
				} else if (viewTile.SW_Tile.mov_dis == -1) {
					viewTile.SW_Tile.mov_dis = viewTile.mov_dis + viewTile.SW_Tile.mov_cost;
					visitedTile.Add (viewTile.SW_Tile);
				}
			}

			if (viewTile.NE_Tile != null && viewTile.NE_Tile.character == null && viewTile.NE_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.NE_Tile.mov_dis != -1 && viewTile.NE_Tile.mov_dis > viewTile.mov_dis + viewTile.NE_Tile.mov_cost) {
					viewTile.NE_Tile.mov_dis = viewTile.mov_dis + viewTile.NE_Tile.mov_cost;
				} else if (viewTile.NE_Tile.mov_dis == -1) {
					viewTile.NE_Tile.mov_dis = viewTile.mov_dis + viewTile.NE_Tile.mov_cost;
					visitedTile.Add (viewTile.NE_Tile);
				}
			}

			if (viewTile.NW_Tile != null && viewTile.NW_Tile.character == null && viewTile.NW_Tile.mov_cost + viewTile.mov_dis <= totalMov) {
				if (viewTile.NW_Tile.mov_dis != -1 && viewTile.NW_Tile.mov_dis > viewTile.mov_dis + viewTile.NW_Tile.mov_cost) {
					viewTile.NW_Tile.mov_dis = viewTile.mov_dis + viewTile.NW_Tile.mov_cost;
				} else if (viewTile.NW_Tile.mov_dis == -1) {
					viewTile.NW_Tile.mov_dis = viewTile.mov_dis + viewTile.NW_Tile.mov_cost;
					visitedTile.Add (viewTile.NW_Tile);
				}
			}

			viewTile = null;
		}
	}

	private void FindAttackablePlayers(HexTile origin) {
		att_range = this.GetComponentInChildren<Weapon> ().Rng;
		GameManager.instance.ResetTileAttDis ();
		GameManager.instance.ResetTilePar ();
		HexTile viewTile = null;
		List<HexTile> visitedTile = new List<HexTile> ();

		this.tile.att_dis = 0;

		visitedTile.Add (origin);
		viewTile = origin;

		while (visitedTile.Count > 0) {
			foreach (HexTile tile in visitedTile) {
				if (viewTile == null || tile.att_dis < viewTile.att_dis) {
					viewTile = tile;
				}
			}

			visitedTile.Remove (viewTile);

			if (viewTile.E_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.E_Tile.character != null && viewTile.E_Tile.character.tag == "Unit") {
						if (!attackablePlayers.Contains (viewTile.E_Tile.character.GetComponentInChildren<Unit> ())) {
							attackablePlayers.Add (viewTile.E_Tile.character.GetComponentInChildren<Unit> ());
						}
					}
				}

				if (viewTile.E_Tile.att_dis == -1) {
					viewTile.E_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.E_Tile);
				} else if (viewTile.E_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.E_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.W_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range [i] && viewTile.W_Tile.character != null && viewTile.W_Tile.character.tag == "Unit") {
						if (!attackablePlayers.Contains (viewTile.W_Tile.character.GetComponentInChildren<Unit> ())) {
							attackablePlayers.Add (viewTile.W_Tile.character.GetComponentInChildren<Unit> ());
						}
					}
				}

				if (viewTile.W_Tile.att_dis == -1) {
					viewTile.W_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.W_Tile);
				} else if (viewTile.W_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.W_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.NE_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.NE_Tile.character != null && viewTile.NE_Tile.character.tag == "Unit") {
						if (!attackablePlayers.Contains (viewTile.NE_Tile.character.GetComponentInChildren<Unit> ())) {
							attackablePlayers.Add (viewTile.NE_Tile.character.GetComponentInChildren<Unit> ());
						}
					}
				}

				if (viewTile.NE_Tile.att_dis == -1) {
					viewTile.NE_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.NE_Tile);
				} else if (viewTile.NE_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.NE_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.NW_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.NW_Tile.character != null && viewTile.NW_Tile.character.tag == "Unit") {
						if (!attackablePlayers.Contains (viewTile.NW_Tile.character.GetComponentInChildren<Unit> ())) {
							attackablePlayers.Add (viewTile.NW_Tile.character.GetComponentInChildren<Unit> ());
						}
					}
				}

				if (viewTile.NW_Tile.att_dis == -1) {
					viewTile.NW_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.NW_Tile);
				} else if (viewTile.NW_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.NW_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.SE_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.SE_Tile.character != null && viewTile.SE_Tile.character.tag == "Unit") {
						if (!attackablePlayers.Contains (viewTile.SE_Tile.character.GetComponentInChildren<Unit> ())) {
							attackablePlayers.Add (viewTile.SE_Tile.character.GetComponentInChildren<Unit> ());
						}
					}
				}

				if (viewTile.SE_Tile.att_dis == -1) {
					viewTile.SE_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.SE_Tile);
				} else if (viewTile.SE_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.SE_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			if (viewTile.SW_Tile != null && viewTile.att_dis + 1 <= att_range.ToArray()[att_range.Count-1]) {
				for (int i = 0; i < att_range.Count; i++) {
					if (viewTile.att_dis + 1 == att_range[i] && viewTile.SW_Tile.character != null && viewTile.SW_Tile.character.tag == "Unit") {
						if (!attackablePlayers.Contains (viewTile.SW_Tile.character.GetComponentInChildren<Unit> ())) {
							attackablePlayers.Add (viewTile.SW_Tile.character.GetComponentInChildren<Unit> ());
						}
					}
				}

				if (viewTile.SW_Tile.att_dis == -1) {
					viewTile.SW_Tile.att_dis = viewTile.att_dis + 1;
					visitedTile.Add (viewTile.SW_Tile);
				} else if (viewTile.SW_Tile.att_dis > viewTile.att_dis + 1) {
					viewTile.SW_Tile.att_dis = viewTile.att_dis + 1;
				}
			}

			viewTile = null;
		}
	}

	private void MoveTowardsPlayer(Unit player) {

	}

    /*private void MoveToEnemy()
    {
        // take visited/enemy list and should be able to traverse to that tile
        // use stack of nodes and keep calling parent until at current tile
        // then transform from current tile to top of stack.
        // keep popping off stack


        // pick an enemy at random
        System.Random rand = new System.Random();

        SearchNode<HexTile, Action> currentTile;
        if (enemyList.Count > 0)
        {
            int enemyToMoveTo = rand.Next(0, enemyList.Count);
            currentTile = enemyList[enemyToMoveTo];
        }
        else
        {
            // why do i have to do this...damn you dictionaries...
            // convert dictionary to a list then get a random value from it.
            var visitedList = new List<SearchNode<HexTile, Action>>();
            foreach(var vTile in visitedTiles)
            {
                visitedList.Add(vTile.Value);
            }
            int tileToMoveTo = rand.Next(0, visitedTiles.Count);
            currentTile = visitedList.ToArray().GetValue(tileToMoveTo) as SearchNode<HexTile, Action>;
        }
        

        // set up the stack for movement
        Stack<SearchNode<HexTile, Action>> enemyStack = new Stack<SearchNode<HexTile, Action>>();
        while (currentTile.state != tile)
        {
            enemyStack.Push(currentTile);
            currentTile = currentTile.parent;
        }

        // move on the respective tiles starting at current position
        while (enemyStack.Count > 0 || movement > 0)
        {
            transform.position = enemyStack.Pop().state.transform.position; // do fancy animations here
            movement--;
        }

    }

    private void CalculateAvailableMovementTiles()
    {
        // add current node to frontier
        SearchNode<HexTile, Action > currentNode = new SearchNode<HexTile, Action>(null, 0, 0, tile, null);
        frontier.Enqueue(currentNode, currentNode.g);

        do
        {
            Debug.Log("frontier size: " + frontier.Size());
            Debug.Log("visited list size: " + visitedTiles.Count);
            currentNode = frontier.Dequeue(); // get first node on frontier

            if (!checkForGoalState(currentNode)) // if cost is already at max movement, then done with the tile
            {
                if (visitedTiles.ContainsKey(currentNode.state)) // check if we've visited this tile before
                {
                    SearchNode<HexTile, Action> conflictingTile;
                    visitedTiles.TryGetValue(currentNode.state, out conflictingTile); // get the cost of the same tile from previous visits

                    if (conflictingTile.g > currentNode.g) // check if the conflicting costs is greater than the current cost
                    {
                        Debug.Log("Visting tile with less cost.");
                        visitedTiles.Remove(currentNode.state);
                        //visitedTiles.Add(currentNode.state, currentNode);
                    }
                }
                else // we've never been here before
                {
                    visitedTiles.Add(currentNode.state, currentNode);
                }


                tile.FindNeighbors(); // get all the neighbors
                foreach (HexTile hexTile in tile.neighbors) // loop through each neighbor
                {
                    if (hexTile != null) // do stuff it neighbor exists
                    {
                        float cost = currentNode.g + hexTile.mov_cost; // accumulate the total cost
                        SearchNode<HexTile, Action> node = new SearchNode<HexTile, Action>(currentNode, cost, 0, hexTile, null); // create a new node for the current neighbor

                        if (hexTile.character != null) // if the neighbor has a unit on it, mark it as visited so we can't accidently move there
                        {
                            Debug.Log("Found " + hexTile.character.name);
                            enemyList.Add(node);
                            visitedTiles.Add(currentNode.state, currentNode);
                        } // end inner if

                        if (visitedTiles.ContainsKey(hexTile)) // don't want to revisit if the current cost is greater (or it contains an unit)
                        {
                            // Debug.Log("Found visited tile: " + hexTile.name);
                            SearchNode<HexTile, Action> conflictingTile;
                            visitedTiles.TryGetValue(currentNode.state, out conflictingTile); // get the cost of the tile we've visted

                            if (currentNode.g < conflictingTile.g) // and compare it to the current cost
                            {
                                Debug.Log("Adding to frontier " + node.state.name + "width cost of " + cost);
                                frontier.Enqueue(node, cost);
                            }
                        } // end inner if
                        else // we've never been here before
                        {
                            Debug.Log("Adding " + node.state.name + " with cost of " + cost + " to frontier.");
                            frontier.Enqueue(node, cost);
                        }
                        
                    } // end outter if
                } // end for each


                //Debug.Log("Visited " + currentNode.state);
            }


        } while (!frontier.IsEmpty);

        Debug.Log("Done calculating movement squares.");
    }

    private Boolean checkForGoalState(SearchNode<HexTile, Action> currentNode)
    {
        return currentNode.g == movement;
    }*/

    void Update(){
		if (Status == 2 && (this.transform.position.x != tile.transform.position.x || this.transform.position.z != tile.transform.position.z)) {
			this.transform.position = Vector3.MoveTowards (this.transform.position, new Vector3(tile.transform.position.x, this.transform.position.y, tile.transform.position.z), 3 * Time.deltaTime);
			if (this.transform.position.x == tile.transform.position.x && this.transform.position.z == tile.transform.position.z) {
				if (movement == 0) {
					Status = 0;
				} else {
					Status = 1;
				}
			}
		}
	}

	public void Death() {
		GameManager.instance.enemyL.Remove (this);
		Destroy (this.gameObject);
	}

	public void ResetMovement() {
		movement = enemy_stats.Mov;
	}
}
