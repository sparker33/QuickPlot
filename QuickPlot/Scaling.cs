using System;
using System.Collections.Generic;

namespace QuickPlot
{
    public class Scaling
    {
        // Private objects
        private int _scaleMode;
        public static List<string> ScaleOptions = new List<string>(new string[] {"Shared",
            "Independent"});
        
        // Public objects
        public string ScaleMode
        {
            get{return ScaleOptions[_scaleMode];}
            set{_scaleMode = ScaleOptions.IndexOf(value);}
        }

		// Class Constructor method
        public Scaling(string scaleID)
        {
            if (scaleID == ScaleOptions[0])
            {
                _scaleMode = 0;
            }
            else if (scaleID == ScaleOptions[1])
            {
                _scaleMode = 1;
            }
            else
            {
                throw new Exception("Invalid scaling selection");
            }
        }
    }
}