using System;
using System.Collections.Generic;
using System.Text;

namespace Artificial_Neural_Network {
	class Signal : List<double> {
		public Signal() { }

		public Signal(int size) {
			for(int i = 0; i < size; i++) {
				this.Add(0.0);
			}
		}

		public Signal(Signal signal) {
			this.AddRange(signal);
		}

		public void AddBias() {
			this.Insert(0, 1);
		}

		public void RemoveBias() {
			this.RemoveAt(0);
		}

		public override String ToString() {
			String toString = "[";

			for(int i = 0; i < this.Count; i++) {
				toString += "(" + String.Format("{0:0.000}", this[i]) + ")";
			}

			toString += "]";

			return toString;
		}
	}
}