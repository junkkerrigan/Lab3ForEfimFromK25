using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public interface XMLAnalyzingStrategy
    {
        void GetXMLData(string file);
        ResultOfParsing Analyze(FilterOfDish filter);
    }
}
