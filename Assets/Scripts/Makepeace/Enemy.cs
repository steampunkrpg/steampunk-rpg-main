using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Enemy : MonoBehaviour {

	public HexTile tile;
	public Stats enemy_stats;

	public bool moving = false;
	public bool Active = false;
	public float movement;

    PriorityQueue<float, SearchNode<HexTile, Action>> frontier = new PriorityQueue<float, SearchNode<HexTile, Action>>();
    List<SearchNode<HexTile, Action>> enemyList = new List<SearchNode<HexTile, Action>>();
    Dictionary<HexTile, SearchNode<HexTile, Action>> visitedTiles = new Dictionary<HexTile, SearchNode<HexTile, Action>>();

	void Start() {
		enemy_stats = this.GetComponentInChildren<Stats> ();

	}

	public void InitPosition () {
		this.transform.position = tile.transform.position;
		this.transform.Translate (new Vector3 (0.0f, 0.5f, 0.0f));
	}

	public void MoveEnemy() {

        if (movement == 0)
        {
            Active = false;
        }
        else
        {
            CalculateAvailableMovementTiles();
            MoveToEnemy();
        }

	}

    private void MoveToEnemy()
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
    }

    void Update(){
		if (moving && (this.transform.position.x != tile.transform.position.x || this.transform.position.z != tile.transform.position.z)) {
			this.transform.position = Vector3.MoveTowards (this.transform.position, new Vector3(tile.transform.position.x, this.transform.position.y, tile.transform.position.z), 3 * Time.deltaTime);
			if (this.transform.position.x == tile.transform.position.x && this.transform.position.z == tile.transform.position.z) {
				moving = false;
			}
		}
	}

	public void ResetMovement() {
		movement = enemy_stats.MOV;
	}
}
