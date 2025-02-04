using System;
using System.Collections.Generic;

namespace Blagodat.Models;

public partial class Connectionstatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Connection> Connections { get; set; } = new List<Connection>();
}
