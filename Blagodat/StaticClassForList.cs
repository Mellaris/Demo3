using Blagodat.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
namespace Blagodat;

public class StaticClassForList
{
    public static List<Client> listClientov = new List<Client>();
    public static List<Employee> listEmployee = new List<Employee>();
    public static List<Job> listJob = new List<Job>();
    public static List<Connection> listConnection = new List<Connection>();
    public static List<Status> listStatus = new List<Status>();
    public static List<Connectionstatus> listStatusConnect = new List<Connectionstatus>();
    public static List<Serviceandorder> listServiceandorder = new List<Serviceandorder>();
    public static List<Service> listService = new List<Service>();
    public static List<Listservice> listListservice = new List<Listservice>();
    
    
    public static bool IsLoginBlocked {  get; set; } = false;
    public static bool checkWhoPerson = false;
    public static int idPerson;
}