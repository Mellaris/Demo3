using System.Linq;
using System.Security.Cryptography;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Blagodat.Models;
using Npgsql.Replication;

namespace Blagodat;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Baza();
    }

    private void Baza()
    {
        StaticClassForList.listClientov.Clear();
        StaticClassForList.listClientov = Helper.DbContext.Clients.Select(clients => new Client
        {
            Id = clients.Id,
            Lastname = clients.Lastname,
            Firstname = clients.Firstname,
            Patronymic = clients.Patronymic,
            Mail = clients.Mail,
            Idadress = clients.Idadress,
            Datahb = clients.Datahb,
            Idpasport = clients.Idpasport,
            Password = clients.Password,
        }).ToList();
    }
    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        string passwords = password.Text;
        string logins = login.Text;

        foreach (Client c in StaticClassForList.listClientov)
        {
            if (c.Password == passwords && c.Mail == logins)
            {
                new Personaly().Show();
                Close();
            }
        }
    }
}