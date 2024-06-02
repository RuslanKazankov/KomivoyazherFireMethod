using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomivoyazherFireMethod
{
    public interface IKomiParams
    {
        double getStartTemperature();
        double getEndTemperature();
        double getCoolingRate();
    }
}
