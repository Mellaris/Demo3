using System;
using System.Collections.Generic;

namespace Blagodat.Models;

public partial class Connection
{
    public int Id { get; set; }

    public int Idemployees { get; set; }

    public DateTime Dataconnection { get; set; }

    public int Idstatus { get; set; }

    public virtual Employee IdemployeesNavigation { get; set; } = null!;

    public virtual Connectionstatus IdstatusNavigation { get; set; } = null!;
}
