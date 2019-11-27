using System;
using System.Collections.Generic;
using System.Text;

namespace Trafiki_ModelLib
{
    public class Bil
    {
        private DateTime _tidspunkt;
        private int _nummer;

        public Bil()
        {
            
        }

        public Bil(DateTime tidspunkt, int nummer)
        {
            _tidspunkt = tidspunkt;
            _nummer = nummer;
        }


        public int Nummer
        {
            get => _nummer;
            set => _nummer = value;
        }

        public DateTime Tidspunkt
        {
            get => _tidspunkt;
            set => _tidspunkt = value;
        }

        public override string ToString()
        {
            return $"{nameof(Tidspunkt)}: {Tidspunkt}, {nameof(Nummer)}: {Nummer}";
        }
    }
}
