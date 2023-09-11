using System.Collections.Generic;

namespace Raman.Core
{
    public class Canvas
    {
        private static Canvas instance = null;
        
        private static readonly object lockObject = new object();

        private Canvas()
        {
            // Private constructor to prevent instantiation from outside
        }

        // Chat GPT: how to implement singleton pattern in c#
        public static Canvas Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new Canvas();
                        }
                    }
                }
                return instance;
            }
        }

        public List<Chart> Charts { get; set; } = new List<Chart>();
        
        public void Refresh()
        {
            Charts.ForEach(x => x.Draw(this));
        }
    }
}