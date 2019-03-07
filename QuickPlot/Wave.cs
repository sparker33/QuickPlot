using System;
using System.Collections.Generic;

namespace QuickPlot
{
    public class Wave
    {
        // Constants
        public static double[] ScalingCoefficients = new double[4] {(1.0d + Math.Sqrt(3.0d)) / Math.Sqrt(32.0d),
            (3.0d + Math.Sqrt(3.0d)) / Math.Sqrt(32.0d),
            (3.0d - Math.Sqrt(3.0d)) / Math.Sqrt(32.0d),
            (1.0d - Math.Sqrt(3.0d)) / Math.Sqrt(32.0d)};
        public static double[] WaveletCoefficients = new double[4] { (1.0d - Math.Sqrt(3.0d)) / Math.Sqrt(32.0d),
            (Math.Sqrt(3.0d) - 3.0d) / Math.Sqrt(32.0d),
            (3.0d + Math.Sqrt(3.0d)) / Math.Sqrt(32.0d),
            (-1.0d - Math.Sqrt(3.0d)) / Math.Sqrt(32.0d)};

        // Private objects
        private int _waveMode;
        public static List<string> WaveOptions = new List<string>(new string[] {"Haar",
            "Daubechies4"});

        // Public objects
        public string WaveMode
        {
            get{return WaveOptions[_waveMode];}
            set{_waveMode = WaveOptions.IndexOf(value);}
        }

		// Class Constructor method
        public Wave(string waveID)
        {
            if (waveID == WaveOptions[0])
            {
                _waveMode = 0;
            }
            else if (waveID == WaveOptions[1])
            {
                _waveMode = 1;
            }
            else
            {
                throw new Exception("Invalid wave type selection");
            }
        }

        // General public method to transform input series to (out) frequencies.
        // Also generates downsampled output result
        // Both outuputs are empty for input sets too short for the chosen transform
        public IEnumerable<double> Transform(IEnumerable<double> input, out IList<double> frequencies)
        {
            List<double> output = new List<double>();
            frequencies = new List<double>();
            frequencies.Clear();
            IEnumerator<double> inputEnumerator = input.GetEnumerator();
            switch (_waveMode)
            {
                case 0: // Haar
                    PointD inPairs = new PointD();
                    PointD outPairs = new PointD();
                    while (inputEnumerator.MoveNext())
                    {
                        inPairs.X = inputEnumerator.Current;
                        if (inputEnumerator.MoveNext())
                        {
                            inPairs.Y = inputEnumerator.Current;
                            outPairs = HaarTransform(inPairs);
                            output.Add(outPairs.X);
                            frequencies.Add(outPairs.Y);
                        }
                    }
                    break;
                case 1: //Daubechies4
                    double[] db4InputSet = new double[4];
                    int i = 0;
                    while (i < 4)
                    {
                        if (inputEnumerator.MoveNext())
                        {
                            db4InputSet[i++] = inputEnumerator.Current;
                        }
                        else { break;}
                    }
                    if (i == 4)
                    {
                        output.Add(Db4TransformS(db4InputSet));
                        frequencies.Add(Db4TransformD(db4InputSet));
                    }
                    else if (i == 3)
                    {
                        db4InputSet[3] = db4InputSet[0];
                        output.Add(Db4TransformS(db4InputSet));
                        frequencies.Add(Db4TransformD(db4InputSet));
                    }
                    else if (i == 2)
                    {
                        db4InputSet[2] = db4InputSet[0];
                        db4InputSet[3] = db4InputSet[1];
                    }
                    else if (i == 1) { break;}
                    else if (i == 0) { throw new Exception("No inputs provided for Transform.");}

                    bool isEvenLength = true;
                    while (inputEnumerator.MoveNext())
                    {
                        db4InputSet[0] = db4InputSet[2];
                        db4InputSet[1] = db4InputSet[3];
                        db4InputSet[2] = inputEnumerator.Current;
                        if (inputEnumerator.MoveNext())
                        {
                            db4InputSet[3] = inputEnumerator.Current;
                        }
                        else
                        {
                            inputEnumerator.Reset();
                            inputEnumerator.MoveNext();
                            db4InputSet[3] = inputEnumerator.Current;
                            isEvenLength = false;
                            break;
                        }
                        output.Add(Db4TransformS(db4InputSet));
                        frequencies.Add(Db4TransformD(db4InputSet));
                    }
                    if (isEvenLength)
                    {
                        db4InputSet[0] = db4InputSet[2];
                        db4InputSet[1] = db4InputSet[3];
                        inputEnumerator.Reset();
                        inputEnumerator.MoveNext();
                        db4InputSet[2] = inputEnumerator.Current;
                        inputEnumerator.MoveNext();
                        db4InputSet[3] = inputEnumerator.Current;
                        output.Add(Db4TransformS(db4InputSet));
                        frequencies.Add(Db4TransformD(db4InputSet));
                    }
                    break;
                default:
                    throw new Exception("Wave type error in Transform call.");
            }

            return output;
        }

        // Haar wavelet-specific transform behavior helper function
        private PointD HaarTransform(PointD input)
        {
            PointD output = new PointD();
            output.X = (input.X + input.Y) / 2.0d;
            output.Y = (input.Y - input.X) / 2.0d;

            return output;
        }

        // Collection of helper functions for Daubechies-4 wavelet-specific transform
        private double Db4TransformS(double[] db4Inputs)
        {
            double S = Wave.ScalingCoefficients[0] * db4Inputs[0]
                + Wave.ScalingCoefficients[1] * db4Inputs[1]
                + Wave.ScalingCoefficients[2] * db4Inputs[2]
                + Wave.ScalingCoefficients[3] * db4Inputs[3];
            return S;
        }
        private double Db4TransformD(double[] db4Inputs)
        {
            double D =  Wave.WaveletCoefficients[0] * db4Inputs[0]
                + Wave.WaveletCoefficients[1] * db4Inputs[1]
                + Wave.WaveletCoefficients[2] * db4Inputs[2]
                + Wave.WaveletCoefficients[3] * db4Inputs[3];
            return D;
        }

        // General public method to inverse transform input averages and frequency data
        public IEnumerable<double> InverseTransform(IEnumerable<double> input, IEnumerable<double> frequencies)
        {
            List<double> output = new List<double>();
            IEnumerator<double> inputEnumerator = input.GetEnumerator();
            IEnumerator<double> frequenciesEnumerator = frequencies.GetEnumerator();
            switch (_waveMode)
            {
                case 0: // Haar
                    PointD inPairs = new PointD();
                    PointD outPairs = new PointD();
                    while (inputEnumerator.MoveNext())
                    {
                        inPairs.X = inputEnumerator.Current;
                        if (!frequenciesEnumerator.MoveNext())
                        {
                            throw new Exception("Inverse Haar inputs length does not match frequencies length.");
                        }
                        inPairs.Y = frequenciesEnumerator.Current;
                        outPairs = HaarInverseTransform(inPairs);
                        output.Add(outPairs.X);
                        output.Add(outPairs.Y);
                    }
                    break;
                case 1: //Daubechies4
                    double[] idb4InputSet = new double[4];

                    if (!inputEnumerator.MoveNext())
                    {
                        throw new Exception("Inverse Db4 not given any inputs.");
                    }
                    idb4InputSet[0] = inputEnumerator.Current;
                    if (!frequenciesEnumerator.MoveNext())
                    {
                        throw new Exception("Inverse Db4 not given any frequencies.");
                    }
                    idb4InputSet[1] = frequenciesEnumerator.Current;
                    idb4InputSet[2] = idb4InputSet[0];
                    idb4InputSet[3] = idb4InputSet[1];
                    output.AddRange(InvDb4Transform(idb4InputSet));
                    
                    while (inputEnumerator.MoveNext())
                    {
                        idb4InputSet[0] = idb4InputSet[2];
                        idb4InputSet[1] = idb4InputSet[3];
                        idb4InputSet[2] = inputEnumerator.Current;
                        if (!frequenciesEnumerator.MoveNext())
                        {
                            throw new Exception("Inverse Db4 inputs length does not match frequencies length.");
                        }
                        idb4InputSet[3] = frequenciesEnumerator.Current;
                        output.AddRange(InvDb4Transform(idb4InputSet));
                    }
                    break;
                default:
                    throw new Exception("Wave type error in Transform call.");
            }

            return output;
        }

        // Haar wavelet-specific inverse transform behavior helper function
        private PointD HaarInverseTransform(PointD input)
        {
            PointD output = new PointD();
            output.X = input.X - input.Y;
            output.Y = input.X + input.Y;

            return output;
        }

        // Daubechies4 wavelet-specific inverse transform behavior helper function
        private IEnumerable<double> InvDb4Transform(double[] idb4Inputs)
        {
            List<double> output = new List<double>();
            output.Add(Wave.ScalingCoefficients[2] * idb4Inputs[0]
                + Wave.WaveletCoefficients[2] * idb4Inputs[1]
                + Wave.ScalingCoefficients[0] * idb4Inputs[2]
                + Wave.WaveletCoefficients[0] * idb4Inputs[3]);
            output.Add(Wave.ScalingCoefficients[3] * idb4Inputs[0]
                + Wave.WaveletCoefficients[3] * idb4Inputs[1]
                + Wave.ScalingCoefficients[1] * idb4Inputs[2]
                + Wave.WaveletCoefficients[1] * idb4Inputs[3]);

            return output;
        }
    }
}