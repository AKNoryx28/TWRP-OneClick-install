using Tools.Fastboot;
using System.IO;

// var fb = new Fastboot();
// fb.Connect();
// var product = fb.Command("getvar:unlocked");
// Console.WriteLine(fb.GetSerialNum() + "\n" + product.Payload + "\n" + product.Status);

string VER = "0.0.1";
var fastboot = new Fastboot();
bool x = false;
string FilePath;

try
{
     fastboot.Connect();
}
catch (System.Exception)
{
    Console.WriteLine("Failed to connect \nMake sure your device is connected on Fastboot Mode");
    Thread.Sleep(2000);
    return;
}

while (!x)
{
    Console.Clear();
    inf();
    Console.WriteLine("\t\tMENU\t");
    Console.Write("\n\t[1] Flash VBMETA\t\t\t[0] Exit\n \n\t[2] Flash TWRP\n \n\t[3] Check device serial\n \n\t[4] Check Bootloader\n");
    Console.Write("\n\tChoice a Number: ");
    string? choice = Console.ReadLine();
    if (choice != null && int.TryParse(choice, out int input))
    {
        switch (input)
        {
            case 1:
                Console.Write("\n\t\tInput vbmeta file location or drag and drop hire \n\t\t::= ");
                FilePath = Console.ReadLine();
                var successVb = Flash(FilePath, "vbmeta");
                if (successVb)
                    Console.WriteLine($"\n\t\tFlash {FilePath} Success");
                else
                    Console.WriteLine($"\n\t\tFlash Failed");
                Thread.Sleep(3000);
                break;
            case 2:
                Console.Write("\n\t\tInput twrp file location or drag and drop hire \n\t\t::= ");
                FilePath = Console.ReadLine();
                var successTw = Flash(FilePath, "recovery");
                if (successTw)
                    Console.WriteLine($"\n\t\tFlash {FilePath} Success");
                else
                    Console.WriteLine($"\n\t\tFlash Failed");
                Thread.Sleep(3000);
                break;
            case 3:
                Console.WriteLine($"\n\t\tSerial Number: {fastboot.GetSerialNum()}");
                Thread.Sleep(5000);
                break;
            case 4:
                if (CheckBootloader())
                    Console.WriteLine("\n\t\tYour device UNLOCKED");
                else
                    Console.WriteLine("\n\t\tYour device LOCKED");
                Thread.Sleep(2000);
                break;
            case 0:
                fastboot.Disconnect();
                x = true;
                break;
            default:
                Console.WriteLine(" Wrong Choice !");
                break;
        }
    }
}

bool CheckBootloader()
{
    if (fastboot.Command("getvar:unlocked").Payload == "yes")
        return true;
    return false;
}

bool Flash(string twrp_img, string partition)
{
    if (File.Exists(twrp_img) && Path.GetExtension(twrp_img) == ".img")
    {
        fastboot.UploadData(twrp_img);
        var a = fastboot.Command($"flash:{partition}");
        if (a.Status.ToString() == "Okay")
            return true;
    }
    else
        throw new Exception($"File {twrp_img} does't exist! or not .img file. \nPlease input the right location");
    return false;
}



void inf()
{
    Console.Write(@$"
    ############################################################################
    #                                                                          #
    #                         TWRP Recovery Installer                          #
    #                                 {VER}                                    #
    #                                  By                                      #
    #                                AUTHOR                                    #
    #                                                                          #
    ############################################################################" + "\n");
}
