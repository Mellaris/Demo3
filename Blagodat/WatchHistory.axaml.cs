using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Blagodat.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Blagodat;

public partial class WatchHistory : Window
{
    private List<Employee> employees = new List<Employee>();
    public WatchHistory()
    {
        InitializeComponent();
        Baza();
        ListEnter.ItemsSource = StaticClassForList.listConnection.Select(connect => new
        {
            EmployeeMail = StaticClassForList.listEmployee.FirstOrDefault(emp => emp.Id == connect.Idemployees)?.Mail,
            ConnectionStatus = StaticClassForList.listStatusConnect.FirstOrDefault(status => status.Id == connect.Idstatus)?.Name,
            ConnectionDate = connect.Dataconnection
        }).ToList();
        employees = Helper.DbContext.Employees.Select(empl => new Employee
        {
            Mail = empl.Mail,
        }).ToList();
        Box.ItemsSource = employees;
    }
    private void ComboBox_SelectionChanged_1(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
    }
    private void ComboBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        var selectedItem = Sortirovka.SelectedItem as ComboBoxItem;
        var selectedText = selectedItem?.Content.ToString();

        List<ConnectionHistoryItem> sortedList;

        if (selectedText == "По возрастанию")
        {
            // ���������� �� �����������
            sortedList = StaticClassForList.listConnection
                .OrderBy(connect => connect.Dataconnection)
                .Select(connect => new ConnectionHistoryItem
                {
                    EmployeeMail = StaticClassForList.listEmployee.FirstOrDefault(emp => emp.Id == connect.Idemployees)?.Mail,
                    ConnectionStatus = StaticClassForList.listStatusConnect.FirstOrDefault(status => status.Id == connect.Idstatus)?.Name,
                    ConnectionDate = connect.Dataconnection
                }).ToList();
        }
        else if (selectedText == "По убыванию")
        {
            // ���������� �� ��������
            sortedList = StaticClassForList.listConnection
                .OrderByDescending(connect => connect.Dataconnection)
                .Select(connect => new ConnectionHistoryItem
                {
                    EmployeeMail = StaticClassForList.listEmployee.FirstOrDefault(emp => emp.Id == connect.Idemployees)?.Mail,
                    ConnectionStatus = StaticClassForList.listStatusConnect.FirstOrDefault(status => status.Id == connect.Idstatus)?.Name,
                    ConnectionDate = connect.Dataconnection
                }).ToList();
        }
        else
        {
            sortedList = StaticClassForList.listConnection
                .Select(connect => new ConnectionHistoryItem
                {
                    EmployeeMail = StaticClassForList.listEmployee.FirstOrDefault(emp => emp.Id == connect.Idemployees)?.Mail,
                    ConnectionStatus = StaticClassForList.listStatusConnect.FirstOrDefault(status => status.Id == connect.Idstatus)?.Name,
                    ConnectionDate = connect.Dataconnection
                }).ToList();
        }

        // ��������� ListBox � ���������������� �������
        ListEnter.ItemsSource = sortedList;
    }

    private void Button_ClickBack(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new Personaly().Show();
        Close();
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
            IdjobNavigation = employee.IdjobNavigation,
            Lastname = employee.Lastname,
            Firstname = employee.Firstname,
            Patronymic = employee.Patronymic,
            Connections = employee.Connections,
        }).ToList();

        StaticClassForList.listJob.Clear();
        StaticClassForList.listJob = Helper.DbContext.Jobs.Select(job => new Job
        {
            Id = job.Id,
            Name = job.Name,
        }).ToList();

        StaticClassForList.listConnection.Clear();
        StaticClassForList.listConnection = Helper.DbContext.Connections.Select(connect => new Connection
        {
            Id = connect.Id,
            Idemployees = connect.Idemployees,
            Dataconnection = connect.Dataconnection,
            Idstatus = connect.Idstatus,
        }).ToList();

        StaticClassForList.listStatusConnect.Clear();
        StaticClassForList.listStatusConnect = Helper.DbContext.Connectionstatuses.Select(status => new Connectionstatus
        {
            Id = status.Id,
            Name = status.Name,
        }).ToList();
    }

    
}

public class ConnectionHistoryItem
{
    public string EmployeeMail { get; set; }
    public string ConnectionStatus { get; set; }
    public DateTime ConnectionDate { get; set; }
}