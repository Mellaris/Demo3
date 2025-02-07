using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Avalonia.Interactivity;
using Blagodat.Models;

namespace Blagodat;

public partial class Personaly : Window
{
    private Timer _sessionTimer;
    private TimeSpan _remainingTime = TimeSpan.FromMinutes(2);
    private bool _warningShown = false;
    public Personaly()
    {
        InitializeComponent();
        StartSessionTimer();
        Baza();
        if(StaticClassForList.checkWhoPerson == true)
        {
            foreach(Client a in StaticClassForList.listClientov)
            {
                if(a.Id == StaticClassForList.idPerson)
                {
                    loginName.Text = a.Firstname;
                    loginLastName.Text = a.Lastname;
                    loginRol.Text = "������������";
                }
            }
        }
        else
        {
            foreach (Employee b in StaticClassForList.listEmployee)
            {
                if (b.Id == StaticClassForList.idPerson)
                {
                    loginName.Text = b.Firstname;
                    loginLastName.Text = b.Lastname;

                    var job = StaticClassForList.listJob.FirstOrDefault(j => j.Id == b.Idjob);
                    loginRol.Text = job != null ? job.Name : "����������� ���������";
                }
            }
        }
        if(loginRol.Text == "Администратор")
        {
            history.IsVisible = true;
        }
        if(loginRol.Text == "Продавец" || loginRol.Text == "Старший смены")
        {
            order.IsVisible = true;
        }
    }
    private void Button_ClickWatchHistory(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new WatchHistory().Show();
        Close();
    }
    private void StartSessionTimer()
    {
        _sessionTimer = new Timer(1000);
        _sessionTimer.Elapsed += UpdateTimer;
        _sessionTimer.Start();
    }
    private async void UpdateTimer(object? sender, ElapsedEventArgs e)
    {
        _remainingTime -= TimeSpan.FromSeconds(1);

        Dispatcher.UIThread.Invoke(() =>
        {
            SessionTimerText.Text = $"���������� �����: {_remainingTime.Minutes:D2}:{_remainingTime.Seconds:D2}";
            
            if (_remainingTime.TotalMinutes < 1 && !_warningShown)
            {
                _warningShown = true;
                WarningText.Text = "�� ��������� ������ �������� 1 ������!";
                WarningText.IsVisible = true;
            }

           
            if (_remainingTime.TotalSeconds <= 0)
            {
                EndSession();
            }
        });
    }
    private async void EndSession()
    {
        _sessionTimer.Stop();
        WarningText.Text = "����� ������ �������! ���� ������������ �� 1 ������.";

        StaticClassForList.IsLoginBlocked = true;

        Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromMinutes(1));
            StaticClassForList.IsLoginBlocked = false;
        });

        Dispatcher.UIThread.Invoke(() =>
        {
            new MainWindow().Show();
            Close();
        });
    }
    private void Button_ClickBack(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _sessionTimer.Stop();
        new MainWindow().Show();
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
    }

    private void Order_OnClick(object? sender, RoutedEventArgs e)
    {
        new NewOrder().Show(); 
        Close();
    }
}