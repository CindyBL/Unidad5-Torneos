using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace Deporte.COMMON.Entidades
{
    public abstract class Base
    {
        public ObjectId Id { get; set; }
    }
}
