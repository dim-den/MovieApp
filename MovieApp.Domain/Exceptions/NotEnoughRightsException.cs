using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.Domain.Exceptions
{
    public class NotEnoughRightsException : Exception
    {
        public ClientType Changer { get; set; }
        public ClientType Changed { get; set; }

        public NotEnoughRightsException(ClientType changer, ClientType changed)
        {
            Changer = changer;
            Changed = changed;
        }

        public NotEnoughRightsException(string message, ClientType changer, ClientType changed) : base(message)
        {
            Changer = changer;
            Changed = changed;
        }

        public NotEnoughRightsException(string message, Exception innerException, ClientType changer, ClientType changed) : base(message, innerException)
        {
            Changer = changer;
            Changed = changed;
        }
    }
}
