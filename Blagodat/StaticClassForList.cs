using Blagodat.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
namespace Blagodat;

public class StaticClassForList
{
    public static List<Client> listClientov = new List<Client>();
}