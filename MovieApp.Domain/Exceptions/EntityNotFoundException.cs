﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public int ID { get; set; }

        public EntityNotFoundException(int id)
        {
            ID = id;
        }

        public EntityNotFoundException(string message, int id) : base(message)
        {
            ID = id;
        }

        public EntityNotFoundException(string message, Exception innerException, int id) : base(message, innerException)
        {
            ID = id;
        }
    }
}
