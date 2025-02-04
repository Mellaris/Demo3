using System;
using System.Collections.Generic;

namespace Blagodat.Models;

public partial class Listservice
{
    public int Id { get; set; }

    public int Idorder { get; set; }

    public int Idservice { get; set; }

    public virtual Serviceandorder IdorderNavigation { get; set; } = null!;

    public virtual Service IdserviceNavigation { get; set; } = null!;
}
