using System;
using System.Collections.Generic;

namespace Blagodat.Models;

public partial class Serviceandorder
{
    public int Id { get; set; }

    public DateOnly Datacreate { get; set; }

    public TimeOnly Timeorders { get; set; }

    public int Idclient { get; set; }

    public int Listserv { get; set; }

    public int Idstatus { get; set; }

    public DateOnly? Datacloseorder { get; set; }

    public int Rentaltime { get; set; }

    public virtual Client IdclientNavigation { get; set; } = null!;

    public virtual Status IdstatusNavigation { get; set; } = null!;

    public virtual ICollection<Listservice> Listservices { get; set; } = new List<Listservice>();
}
