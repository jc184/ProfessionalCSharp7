//Mathlib.cs
namespace MathLib
{
    /// <summary>
    /// Wrox.MathLib.Calculator class.
    /// Provides a method to add two doubles
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// The Add method allows us to add two doubles
        /// </summary>
        /// <param name="x">First number to add</param>
        /// <param name="y">Second number to add</param>
        /// <returns>Result of the addition (double)</returns>
        public static double Add(double x, double y) => x + y;

        public static double Subtract(double x, double y) => x - y;
    }
}
