using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticExplorers.JSONUtilities
{
    public class Story
    {
        public string id;
        public string title;
        public string description;

        public List<Choice> choices;
    }
}
