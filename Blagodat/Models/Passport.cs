using System;
using System.Collections.Generic;

namespace Blagodat.Models;

public partial class Passport
{
    public int Id { get; set; }

    public int Series { get; set; }

    public int Number { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
