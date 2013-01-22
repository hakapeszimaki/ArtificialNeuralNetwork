using System;
using System.Collections.Generic;
using System.Text;

namespace Artificial_Neural_Network {
	class Layer : List<Node> {

		public static Signal operator *(Signal signal, Layer layer) {
			Signal result = new Signal(layer.Count);

			signal.AddBias();

			for(int i = 0; i < layer.Count; i++) {
				result[i] = signal * layer[i];
			}

			signal.RemoveBias();

			return result;
		}

		public static Signal GetWeightSum(Signal signal, Layer layer) {
			Signal result = new Signal(layer.Count);

			signal.AddBias();

			for(int i = 0; i < layer.Count; i++) {
				result[i] = Node.GetWeightSum(signal, layer[i]);
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
