using System;
using System.Collections.Generic;

namespace Blagodat.Models;

public partial class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Codservice { get; set; } = null!;

    public int Price { get; set; }

    public virtual ICollection<Listservice> Listservices { get; set; } = new List<Listservice>();
}
