using System;
using System.Collections.Generic;

namespace Blagodat.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Serviceandorder> Serviceandorders { get; set; } = new List<Serviceandorder>();
}
