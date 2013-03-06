using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StatelessViewModels.Models;
using UpdateControls.XAML;

namespace StatelessViewModels.ViewModels
{
    public class ItemDetailViewModel
    {
        private readonly Selection _selection;

        public ItemDetailViewModel(Selection selection)
        {
            _selection = selection;
        }

        public string Name
        {
            get
            {
                if (_selection.SelectedItem == null)
                    return "<No item is selected>";
                else
                    return _selection.SelectedItem.Name;
            }
            set
            {
                if (_selection.SelectedItem != null)
                {
                    _selection.SelectedItem.Name = value;
                }
            }
        }
    }
}
