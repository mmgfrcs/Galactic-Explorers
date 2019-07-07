using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticExplorers.JSONUtilities
{
    [Serializable]
    public class Story
    {
        public string id;
        public string title;
        public List<string> descriptionLine;

        public float weight;

        public List<Choice> choices;
    }
}
