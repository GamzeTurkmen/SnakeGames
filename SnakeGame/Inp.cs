using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public partial class Inp : Component
    {
        public Inp()
        {
            InitializeComponent();
        }

        public Inp(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
