using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disc0ver.Event
{
    public class EventIdAttribute : Attribute
    {
        public string value;

        public EventIdAttribute(string value)
        {
            this.value = value;
        }
    }

}