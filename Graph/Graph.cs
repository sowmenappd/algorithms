using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SRLibs.Graph;

namespace SRLibs.Graph
{
	public class Graph<T>
	{
		static int nodeCount = 0;

		int[,] adjMat = new int[10000,10000];

    public List<Node> nodes;

		public int NodeCount {
			get {
				return nodes.Count;
			}
		}

		public class Node {
			public T value;
			int _idx;

			public Node(){
				nodeCount++;
				_idx = nodeCount - 1;
			}

			public Node(T value){
				nodeCount++;
				_idx = nodeCount - 1;
				this.value = value;
			}
		}
		
		public Graph(){}

		public Graph(int nodes){
			this.nodes = new List<Node>(nodes);
		}

		public void AddNode(T data){
			Node n = new Node(data);
			nodes.Add(n);
		}

	}
}
