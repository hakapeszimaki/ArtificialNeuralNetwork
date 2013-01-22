using System;
using System.Collections.Generic;
using System.Text;

namespace Artificial_Neural_Network {
    class Program {
        static void Main(string[] args) {
			Network network = new Network();
			Layer layer2 = new Layer();
			Layer layer3 = new Layer();
			Node node1 = new Node(2);
			Node node2 = new Node(2);
			Node node3 = new Node(2);

			layer2.Add(node1);
			layer2.Add(node2);
			layer3.Add(node3);

			network.Add(layer2);
			network.Add(layer3);

			Dictionary<Signal, Signal> trainingSet = new Dictionary<Signal, Signal>();
			Signal signalFF = new Signal();
			Signal signalFT = new Signal();
			Signal signalTF = new Signal();
			Signal signalTT = new Signal();
			Signal signalF = new Signal();
			Signal signalT = new Signal();

			signalFF.Add(0.0); signalFF.Add(0.0);
			signalFT.Add(0.0); signalFT.Add(1.0);
			signalTF.Add(1.0); signalTF.Add(0.0);
			signalTT.Add(1.0); signalTT.Add(1.0);

			signalF.Add(0.0);
			signalT.Add(1.0);

			trainingSet.Add(signalFF, signalF);
			trainingSet.Add(signalFT, signalT);
			trainingSet.Add(signalTF, signalT);
			trainingSet.Add(signalTT, signalT);

			network.Train(trainingSet);

			Console.WriteLine("OutputFF: " + signalFF * network);
			Console.WriteLine("OutputFT: " + signalFT * network);
			Console.WriteLine("OutputTF: " + signalTF * network);
			Console.WriteLine("OutputTT: " + signalTT * network);

			Console.ReadLine();
		}
	}
}
