using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Androbot.Androbot.Events
{
    public class EventArgImage : EventArgs
    {
        public Image Image { get; private set; }

        public EventArgImage(Image image)
        {
            this.Image = image;
        }
    }
}
