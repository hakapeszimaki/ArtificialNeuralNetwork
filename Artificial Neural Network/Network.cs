using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Artificial_Neural_Network {
	class Network : List<Layer> {
		public static Random random = new Random();
		private double[][] error;

		public bool Train(Dictionary<Signal, Signal> trainingSet) {
			int iteration = 0;
			double trainingSetError = 0;

			Console.WriteLine("Network before training: " + this);

			do {
				trainingSetError = 0;

				foreach(KeyValuePair<Signal, Signal> trainingData in trainingSet) {
					trainingSetError += OnlineTraining(trainingData);
				}

				trainingSetError /= trainingSet.Count;

				iteration++;

			} while((trainingSetError > 0.001) && (iteration < 10000));

			Console.WriteLine("Network after training:  " + this);
			Console.WriteLine("- iterations: " + iteration);
			Console.WriteLine("- error: " + String.Format("{0:0.000}", trainingSetError));

			return true;
		}

		public double OnlineTraining(KeyValuePair<Signal, Signal> trainingData) {
			double learningRate = 0.5;

			error = new double[][] {
				new double[this[0].Count],
				new double[this[1].Count]
			};
			
			List<List<KeyValuePair<double, double>>> output = activateWeightedSum(GetWeightedSum(trainingData.Key, this));

			int i, j;

			// Calculating errors
			// * output layer
			for(i = 0; i < this[1].Count; i++) {
				error[1][i] = output[1][i].Value * (trainingData.Value[i] - output[1][i].Key);
			}

			// * hidden layer
			for(i = 0; i < this[0].Count; i++) {
				for(j = 0; j < this[1].Count; j++) {
					error[0][i] += error[1][j] * this[1][j][i+1] * output[0][i].Value;
				}
			}

			// Updating weights
			// * hidden layer
			for(i = 0; i < this[0].Count; i++) {
				this[0][i][0] += learningRate * error[0][i];

				for(j = 0; j < this[0][i].Count - 1; j++) {
					this[0][i][j + 1] += learningRate * error[0][i] * trainingData.Key[j];
				}
			}

			// * output layer
			for(i = 0; i < this[1].Count; i++) {
				this[1][i][0] += learningRate * error[1][i];

				for(j = 0; j < this[1][i].Count - 1; j++) {
					this[1][i][j + 1] += learningRate * error[1][i] * output[0][j].Key;
				}
			}

			// Calculating TrainingData cost
			double trainingDataError = 0;

			for(i = 0; i < this[1].Count; i++) {
				trainingDataError += Math.Pow(trainingData.Value[i] - output[1][i].Key, 2);
			}

			return trainingDataError = 0.5 * trainingDataError;
		}

		public static Signal operator *(Signal signal, Network network) {
			for(int i = 0; i < network.Count; i++) {
				signal = signal * network[i];
			}

			return signal;
		}

		private static List<Signal> GetWeightedSum(Signal signal, Network network) {
			List<Signal> weightSums = new List<Signal>();

			for(int i = 0; i < network.Count; i++) {
				weightSums.Add(Layer.GetWeightedSum(signal, network[i]));

				if(i == network.Count - 1)
					break;
				
				signal = signal * network[i];
			}

			return weightSums;
		}

		private List<List<KeyValuePair<double, double>>> activateWeightedSum(List<Signal> weightedSum) {
			int i, j;

			List<List<KeyValuePair<double, double>>> activatedWeightedSum = new List<List<KeyValuePair<double, double>>>();
			
			for(i = 0; i < weightedSum.Count; i++) {
				activatedWeightedSum.Add(new List<KeyValuePair<double, double>>());
				for(j = 0; j < weightedSum[i].Count; j++) {
					activatedWeightedSum[i].Add(new KeyValuePair<double, double>(LogisticFunction(weightedSum[i][j]), LogisticFunctionDerivative(weightedSum[i][j])));
				}
			}

			return activatedWeightedSum;
		}

		public static double LogisticFunction(double value) {
			return 1/(1 + Math.Exp(-value));
		}

		public static double LogisticFunctionDerivative(double value) {
			double logisticValue = LogisticFunction(value);
			return logisticValue * (1 - logisticValue);
		}

		public override String ToString() {
			String toString = "";

			for(int i = 0; i < this.Count; i++) {
				toString += this[i].ToString();
			}

			return toString;
		}
	}
}
