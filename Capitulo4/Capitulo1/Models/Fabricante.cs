﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capitulo1.Models
{
    public class Fabricante
    {
        public int FabricanteId { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }

    }
}