using System;
using System.Windows.Forms;
using Sample.Bus;

namespace Sample.UI
{
    static class Program
    {
        const string ConnectionString = "Data Source=tpe-t60;Initial Catalog=DDDCourse;Integrated Security=SSPI;";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InventoryItemSummaryForm(new MsmqBus(@".\Private$\Commands"), new InventoryService.InventoryService(ConnectionString)));
        }
    }
}
