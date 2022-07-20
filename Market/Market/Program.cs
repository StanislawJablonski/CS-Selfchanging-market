using System;

namespace Market
{
    class Program
    {
        static void Main(string[] args)
        {
            var simulation = new Simulation();
            simulation.Simulate(10);
        }
    }
}