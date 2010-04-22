using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sample.Bus;
using Sample.Commands;
using Sample.InventoryService.DTO;

namespace Sample.UI
{
    public partial class InventoryItemForm : Form
    {
        readonly InventoryItemDTO _inventoryItemDTO;
        readonly IBus _bus;

        public InventoryItemForm(InventoryItemDTO inventoryItemDTO, IBus bus)
        {
            _inventoryItemDTO = inventoryItemDTO;
            _bus = bus;

            InitializeComponent();

            InitializeUI();
        }

        void InitializeUI()
        {
            if (_inventoryItemDTO == null)
                return;

            textBoxName.Text = _inventoryItemDTO.Name;
            textBoxDescription.Text = _inventoryItemDTO.Description;
            textBoxCount.Text = _inventoryItemDTO.Count.ToString();
            radioButtonActive.Checked = _inventoryItemDTO.Active;
            radioButtonInactive.Checked = !_inventoryItemDTO.Active;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var commands = new List<IMessage>();
            if (_inventoryItemDTO == null)
            {
                AddNewCommands(commands);
            }
            else
            {
                AddChangeCommands(commands);
            }

            _bus.Publish(commands);

            Close();
        }

        void AddChangeCommands(ICollection<IMessage> commands)
        {
            if (textBoxName.Text != _inventoryItemDTO.Name || textBoxDescription.Text != _inventoryItemDTO.Description)
                commands.Add(new ChangeInventoryItemDetailsCommand(_inventoryItemDTO.Id, _inventoryItemDTO.Version, textBoxName.Text, textBoxDescription.Text));
            
            int count = Convert.ToInt32(textBoxCount.Text);
            if (count != _inventoryItemDTO.Count)
                commands.Add(new AdjustInventoryCountCommand(_inventoryItemDTO.Id, _inventoryItemDTO.Version, count - _inventoryItemDTO.Count));

            if (_inventoryItemDTO.Active && radioButtonInactive.Checked)
                commands.Add(new DeactivateInventoryItemCommand(_inventoryItemDTO.Id, _inventoryItemDTO.Version));

            if (!_inventoryItemDTO.Active && radioButtonActive.Checked)
                commands.Add(new ActivateInventoryItemCommand ( _inventoryItemDTO.Id, _inventoryItemDTO.Version ));
        }

        void AddNewCommands(ICollection<IMessage> commands)
        {
            int count = Convert.ToInt32(textBoxCount.Text);
            commands.Add(new CreateInventoryItemCommand(Guid.NewGuid(), textBoxName.Text, textBoxDescription.Text, count, radioButtonActive.Checked));
        }
    }
}
