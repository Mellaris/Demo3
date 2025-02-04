using System;
using System.Collections.Generic;

namespace Blagodat.Models;

public partial class Employee
{
    public int Id { get; set; }

    public int Idjob { get; set; }

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Connection> Connections { get; set; } = new List<Connection>();

    public virtual Job IdjobNavigation { get; set; } = null!;
}
