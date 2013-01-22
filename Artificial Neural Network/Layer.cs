using System;
using System.Collections.Generic;
using System.Text;

namespace Artificial_Neural_Network {
	class Layer : List<Node> {

		public static Signal operator *(Signal signal, Layer layer) {
			return GetWeightedSum(signal, layer, false);
		}

		public static Signal GetWeightedSum(Signal signal, Layer layer, bool weightedSum = true) {
			Signal result = new Signal(layer.Count);

			signal.AddBias();

			for(int i = 0; i < layer.Count; i++) {
				if(weightedSum)
					result[i] = Node.GetWeightedSum(signal, layer[i]);
				else
					result[i] = signal * layer[i];
			}

			signal.RemoveBias();

			return result;
		}

		public override String ToString() {
			String toString = "{";

			for(int i = 0; i < this.Count; i++) {
				toString += this[i].ToString();
			}

			toString += "} ";

			return toString;
		}
	}
}
