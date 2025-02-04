using System;
using System.Collections.Generic;

namespace Blagodat.Models;

public partial class Adress
{
    public int Id { get; set; }

    public int Codeemail { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public int Numberstreet { get; set; }

    public int Roomnum { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
