﻿using System;
using System.Collections.Generic;

namespace BE
{
    public class Smoothie : FlavoredProduct
    {
        public bool Search(Dictionary<string, List<Object>> dictionary)
        {
          
            return base.Search(dictionary) ;
        }

    }
}
