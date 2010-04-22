using System;
using System.Linq;
using System.Windows.Forms;
using Sample.Bus;
using Sample.InventoryService;
using Sample.InventoryService.DTO;

namespace Sample.UI
{
    public partial class InventoryItemSummaryForm : Form
    {
        readonly IBus _bus;
        readonly IInventoryService _inventoryService;

        public InventoryItemSummaryForm(IBus bus, IInventoryService inventoryService)
        {
            _bus = bus;
            _inventoryService = inventoryService;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            RefreshData();
        }

        void RefreshData()
        {
            listBoxInventoryItemSummaries.Items.Clear();
            listBoxInventoryItemSummaries.Items.AddRange(_inventoryService.GetSummaries().ToArray());
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            ShowItem(null);
        }

        void ShowItem(InventoryItemDTO dto)
        {
            new InventoryItemForm(dto, _bus).Show();
        }

        private void listBoxInventoryItemSummaries_DoubleClick(object sender, EventArgs e)
        {
            var selectedItem = listBoxInventoryItemSummaries.SelectedItem as InventoryItemSummaryDTO;

            if(selectedItem == null)
                return;

            InventoryItemDTO item = _inventoryService.GetInventoryItem(selectedItem.Id);
            ShowItem(item);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
