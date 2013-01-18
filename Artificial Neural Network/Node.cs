using System;
using System.Collections.Generic;
using System.Text;

namespace Artificial_Neural_Network {
	class Node : Signal {
		public double Error = 0;
		public double WeightedSum = 0;

		public Node(int size) {
			for(int i = 0; i < size + 1; i++) {
				this.Add((Network.random.NextDouble() - 0.5) / 5);
			}
		}

		/*public static double operator *(Signal signal, Node node) {
			return Network.LogisticFunction(GetWeightSum(signal, node));
		}*/

		//public static double GetWeightSum(Signal signal, Node node) {
		public double GetWeightSum(Signal signal) {
			if(this.Count != signal.Count)
				Console.WriteLine("!!!");

			//double WeightedSum = 0;
			WeightedSum = 0;

			for(int i = 0; i < this.Count; i++) {
				WeightedSum += this[i] * signal[i];
			}

			//return WeightedSum;
			return Network.LogisticFunction(WeightedSum);
		}

		public override String ToString() {
			String toString = "[";

			toString += "(B" + String.Format("{0:+0.000;-0.000}", this[0]) + ")";

			for(int i = 1; i < this.Count; i++) {
				toString += "(" + String.Format("{0:+0.000;-0.000}", this[i]) + ")";
			}

			toString += "]";

			return toString;
		}
	}
}
