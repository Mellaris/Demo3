using System;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Blagodat.Models;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using QRCoder;

namespace Blagodat;

public partial class NewOrder : Window
{
    public int help = 0;
    public NewOrder()
    {
        InitializeComponent();
        Baza();
        var sortedOrders =  StaticClassForList.listServiceandorder.OrderBy(order => order.Id).ToList();
        int maxOrderId = sortedOrders.LastOrDefault()?.Id ?? 0;
        maxOrderId = maxOrderId + 1;
        textKod.Text = maxOrderId.ToString();
    }
    private void Button_CheckKod(object? sender, RoutedEventArgs e)
    {
        int kod = Convert.ToInt32(textKod.Text);
        foreach (var a in StaticClassForList.listServiceandorder)
        {
            if (a.Id == kod)
            {
                help = 1;
                break;
            }
        }
        if (help == 0)
        {
            MasText.IsVisible = true;
        }
        help = 0;
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
        
        StaticClassForList.listService.Clear();
        StaticClassForList.listService = Helper.DbContext.Services.Select(serv => new Service
        {
            Id = serv.Id,
            Name = serv.Name,
            Codservice = serv.Codservice,
            Price = serv.Price,
        }).ToList();
        
        StaticClassForList.listServiceandorder.Clear();
        StaticClassForList.listServiceandorder = Helper.DbContext.Serviceandorders.Select(servOrd => new Serviceandorder
        {
            Id = servOrd.Id,
            Datacreate = servOrd.Datacreate,
            Timeorders = servOrd.Timeorders,
            Idclient = servOrd.Idclient,
            Listserv = servOrd.Listserv,
            Idstatus = servOrd.Idstatus,
            Datacloseorder = servOrd.Datacloseorder,
            Rentaltime = servOrd.Rentaltime,
        }).ToList();
        
        StaticClassForList.listListservice.Clear();
        StaticClassForList.listListservice = Helper.DbContext.Listservices.Select(serviceList => new Listservice
        {
            Id = serviceList.Id,
            Idorder = serviceList.Idorder,
            Idservice = serviceList.Idservice,
        }).ToList();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        using (QRCodeData qrCodeData = qrGenerator.CreateQrCode("1434554345", QRCodeGenerator.ECCLevel.Q))
        using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
        {
            byte[] qrCodeImage = qrCode.GetGraphic(20);
            using var memoryStream = new MemoryStream(qrCodeImage);

            var bitmap = new Bitmap(memoryStream); 
            qr.Source = bitmap; 
        }
    }

    private void Button_OnClickPdf(object? sender, RoutedEventArgs e)
    {
        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        using (QRCodeData qrCodeData = qrGenerator.CreateQrCode("1434554345", QRCodeGenerator.ECCLevel.Q))
        using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
        {
            byte[] qrCodeImage = qrCode.GetGraphic(20);
            using var memoryStream = new MemoryStream(qrCodeImage);

            var bitmap = new Bitmap(memoryStream); 
            qr.Source = bitmap; 
            memoryStream.Position = 0;

            string pdfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "QRCode.pdf");
            using (var doc = new PdfDocument())
            {
                var page = doc.AddPage();
                using var gfx = XGraphics.FromPdfPage(page);

                using var image = XImage.FromStream(() => new MemoryStream(memoryStream.ToArray()));
                gfx.DrawImage(image, 50, 50, 200, 200);

                doc.Save(pdfPath);
            }
        }
        
        
    }
}