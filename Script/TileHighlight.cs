﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileHighlight : MonoBehaviour {
	public List<gridHighlight> previousNodeList = new List<gridHighlight>();
	private gridHighlight[] gridSet;
	private Vector2 originTile;
	private Map map;

	void Start() {
		map = GetComponent<Map>();
	}

	public List<gridHighlight> findHighlight(Vector2 original, int point) {
		originTile = original;
		List<gridHighlight> nodes = findConnectNode(original, new List<gridHighlight>(), point );
		previousNodeList = nodes;
		highlightCtrl(nodes, false);
		return nodes;
	}
	
	public void highlightCtrl( List<gridHighlight> nodes, bool isClose ) {
		foreach (gridHighlight n in nodes) {
			if (!isClose) {
				n.tag = "GroundMove";
				n.changeHighLight( Resources.Load<Sprite>("green"), 0.7f, true);
			} else {
				n.tag = "GroundIdle";
				n.changeHighLight( Resources.Load<Sprite>("white"), 0.1f, false);
			}
		}
	}
	
	private List<gridHighlight> findConnectNode(Vector2 node, List<gridHighlight> nodeStorage, float movePoint) {
		movePoint -= map.mapGrid[node].cost;

		if (movePoint > 0) {
			List<Vector2> tempNodeList = new List<Vector2>();
						tempNodeList.Add(new Vector2(node.x+1, node.y));
						tempNodeList.Add(new Vector2(node.x-1, node.y));
						tempNodeList.Add(new Vector2(node.x, node.y+1));
						tempNodeList.Add(new Vector2(node.x, node.y-1));
			List<Vector2> tempNodeList2 = tempNodeList;	


			for (int i = 0; i < tempNodeList.Count; i++) {
					Vector2 n = tempNodeList[i];

				if (!map.mapGrid.ContainsKey(n) || nodeStorage.Contains(map.mapGrid[n]) || n == originTile) {
					tempNodeList2.Remove(n);
					}
				}

			foreach (Vector2 k in tempNodeList2) {
				if (map.mapGrid.ContainsKey(k) && k != originTile) {
					nodeStorage.Add(map.mapGrid[k]);
					findConnectNode(k, nodeStorage, movePoint);
				}
			}
		}
		return nodeStorage;
	}

}
