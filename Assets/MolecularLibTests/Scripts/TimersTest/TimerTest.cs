using MolecularLib;
using MolecularLib.Timers;
using UnityEngine;

namespace MolecularLibTests.Scripts.TimersTest
{
    public class TimerTest : AutoSingleton<TimerTest>
    {
        public float duration = 0.2f;

        private void Start()
        {
            StartTimerWithCallback();
            PerformExpensiveCalculation();
            StartTimerWithReference();
            StartAndStopTimer();
        }

        // Timer with a simple callback when complete
        private void StartTimerWithCallback()
        {
            Debug.Log("Timer Started");
            Timer.TimerAsync(duration, () => Debug.Log("Timer is done!"));
        }

        // Perform an expensive calculation (simulated delay)
        private void PerformExpensiveCalculation()
        {
            Debug.Log("Calculation Started");
            ExpensiveFunction();
            Debug.Log("Calculation Finished");
        }

        // Timer with a reference to handle completion using events
        private void StartTimerWithReference()
        {
            var reference = Timer.TimerAsyncReference(duration);
            reference.OnFinish += () => Debug.Log("Timer with reference is done!");
        }

        // Create a timer and stop it after completion
        private void StartAndStopTimer()
        {
            var timer = Timer.Create(duration, () => Debug.Log("Timer is done!"), true);
            timer.OnComplete += () =>
            {
                Debug.Log("Stopping timer after completion.");
                timer.StopTimer();
            };
        }

        // Expensive computation simulation
        private void ExpensiveFunction()
        {
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 10000; j++) { /* Simulate computation */ }
            }
        }
    }
}