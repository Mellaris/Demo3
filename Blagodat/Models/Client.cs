using System;
using System.Collections.Generic;

namespace Blagodat.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public int Idadress { get; set; }

    public DateOnly Datahb { get; set; }

    public int Idpasport { get; set; }

    public string Password { get; set; } = null!;

    public virtual Adress IdadressNavigation { get; set; } = null!;

    public virtual Passport IdpasportNavigation { get; set; } = null!;

    public virtual ICollection<Serviceandorder> Serviceandorders { get; set; } = new List<Serviceandorder>();
}
