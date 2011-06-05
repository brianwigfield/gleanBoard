using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gleanBoard.Resources;

namespace gleanBoard.Handlers
{
    public class HomeHandler
    {
        public object Get()
        {
            return new Home { Title = "gleanBoard" };
        }
    }
}