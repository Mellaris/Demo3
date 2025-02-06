using System.Linq;
using System.Security.Cryptography;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Blagodat.Models;
using Npgsql.Replication;

namespace Blagodat;

public partial class MainWindow : Window
{
    private bool isPasswordVisible = false; 
    private bool check = false;
    public MainWindow()
    {
        InitializeComponent();
        Baza();
        login.Text = "mironov@namecomp.ru";
        password.Text = "YOyhfR1";
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if (StaticClassForList.IsLoginBlocked)
        {

            WarningText.Text = "Вход временно заблокирован! Попробуйте позже.";
            WarningText.IsVisible = true;
            return;
        }
        string passwords = password.Text;
        string logins = login.Text;
        if(check == false)
        {
            foreach (Client c in StaticClassForList.listClientov)
            {
                if (c.Password == passwords && c.Mail == logins)
                {
                    check = true;
                    StaticClassForList.checkWhoPerson = true;
                    StaticClassForList.idPerson = c.Id;
                    new Personaly().Show();
                    Close();      
                    break;
                }
            }
        }
        if (check == false)
        {
            foreach (Employee a in StaticClassForList.listEmployee)
            {
                if (a.Mail == logins && a.Password == passwords)
                {
                    check = true;
                    StaticClassForList.checkWhoPerson = false;
                    StaticClassForList.idPerson = a.Id;
                    new Personaly().Show();
                    Close();
                    break;
                }
            }
        }
        check = false;
    }

    private void Button_ClickVisible(object? sender, RoutedEventArgs e)
    {
        isPasswordVisible = !isPasswordVisible; 
       
        password.PasswordChar = isPasswordVisible ? '\0' : '●';

        if (sender is Button button)
        {
            button.Content = isPasswordVisible ? "Скрыть" : "Увидеть";
        }
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

        StaticClassForList.listEmployee.Clear();
        StaticClassForList.listEmployee = Helper.DbContext.Employees.Select(employee => new Employee
        {
            Id = employee.Id,
            Mail = employee.Mail,
            Password = employee.Password,
            Idjob = employee.Idjob,
            Lastname = employee.Lastname,
            Firstname = employee.Firstname,
            Patronymic = employee.Patronymic,
        }).ToList();
    }
}