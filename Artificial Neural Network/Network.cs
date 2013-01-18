using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Artificial_Neural_Network {
	class Network : List<Layer> {
		public static Random random = new Random();
		//private double[][] error;

		public Network() { }

		public bool Train(Dictionary<Signal, Signal> trainingSet) {
			/*error = new double[][] {
				new double[this[0].Count],
				new double[this[1].Count]
			};*/

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

			// Nullifying errors
			/*for(int i = 0; i < error.Length; i++) {
				for(int j = 0; j < error[i].Length; j++) {
					error[i][j] = 0;
				}
			}*/
			for(int i = 0; i < this.Count; i++) {
				for(int j = 0; j < this[i].Count; j++) {
					this[i][j].Error = 0;
				}
			}
			
			// Calculating actual output and processing signal
			Signal output = trainingData.Key * this;
			//List<Signal> weightedSum = Network.GetWeightSum(trainingData.Key, this);

			// Calculating errors
			// * output layer
			for(int i = 0; i < this[1].Count; i++) {
				this[1][i].Error = output[i] * (1 - output[i]) * (trainingData.Value[i] - output[i]);
				//error[1][i] = output[i] * (1 - output[i]) * (trainingData.Value[i] - output[i]);
				//error[1][i] = LogisticFunctionDerivative(weightedSum[1][i]) * (trainingData.Value[i] - LogisticFunction(weightedSum[1][i]));
			}

			// * hidden layer
			for(int i = 0; i < this[0].Count; i++) {
				for(int j = 0; j < this[1].Count; j++) {
					this[0][i].Error += this[1][j].Error * this[1][j][i+1] * LogisticFunctionDerivative(this[0][i].WeightedSum);
					//error[0][i] += error[1][j] * this[1][j][i+1] * LogisticFunctionDerivative(weightedSum[i]);
					//error[0][i] += error[1][j] * this[1][j][i+1] * LogisticFunctionDerivative(weightedSum[0][i]);
				}
			}

			// Updating weights
			// * hidden layer
			for(int i = 0; i < this[0].Count; i++) {
				this[0][i][0] += learningRate * this[0][i].Error;
				//this[0][i][0] += learningRate * error[0][i];

				for(int j = 0; j < this[0][i].Count - 1; j++) {
					this[0][i][j + 1] += learningRate * this[0][i].Error * trainingData.Key[j];
					//this[0][i][j + 1] += learningRate * error[0][i] * trainingData.Key[j];
				}
			}

			// * output layer
			for(int i = 0; i < this[1].Count; i++) {
				this[1][i][0] += learningRate * this[1][i].Error;
				//this[1][i][0] += learningRate * error[1][i];

				for(int j = 0; j < this[1][i].Count - 1; j++) {
					this[1][i][j + 1] += learningRate * this[1][i].Error * Network.LogisticFunction(this[0][j].WeightedSum);
					//this[1][i][j + 1] += learningRate * error[1][i] * Network.LogisticFunction(weightedSum[j]);
					//this[1][i][j + 1] += learningRate * error[1][i] * Network.LogisticFunction(weightedSum[0][j]);
				}
			}

			// Nullifying errors
			/*for(int i = 0; i < error.Length; i++) {
				for(int j = 0; j < error[i].Length; j++) {
					error[i][j] = 0;
				}
			}*/
			for(int i = 0; i < this.Count; i++) {
				for(int j = 0; j < this[i].Count; j++) {
					this[i][j].Error = 0;
				}
			}

			// Calculating TrainingData cost
			double trainingDataError = 0;

			for(int i = 0; i < this[1].Count; i++) {
				trainingDataError += Math.Pow(trainingData.Value[i] - output[i], 2);
				//trainingDataError += Math.Pow(trainingData.Value[i] - LogisticFunction(weightedSum[1][i]), 2);
			}

			return trainingDataError = 0.5 * trainingDataError;
		}

		public static Signal operator *(Signal signal, Network network) {
			for(int i = 0; i < network.Count; i++) {
				signal = signal * network[i];
			}

			return signal;
		}

		/*public static List<Signal> GetWeightSum(Signal signal, Network network) {
			List<Signal> weightSums = new List<Signal>();

			for(int i = 0; i < network.Count; i++) {
				weightSums.Add(Layer.GetWeightSum(signal, network[i]));
			}

			return weightSums;
		}*/

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
