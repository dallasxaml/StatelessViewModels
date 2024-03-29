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
    public class MainViewModel
    {
        private readonly Document _document;
		private readonly Selection _selection;

        public MainViewModel(Document document, Selection selection)
        {
            _document = document;
			_selection = selection;
        }

        public string Name
        {
            get { return _document.Name; }
            set { _document.Name = value; }
        }

        public IEnumerable<ItemHeader> Items
        {
            get
            {
                return
                    from item in _document.Items
                    select new ItemHeader(item);
            }
        }

        public ItemHeader SelectedItem
        {
            get
            {
                return _selection.SelectedItem == null
                    ? null
                    : new ItemHeader(_selection.SelectedItem);
            }
            set
            {
                if (value != null)
                {

                    _selection.SelectedItem = value.Item;
                }
            }
        }

        public ItemViewModel ItemDetail
        {
            get
            {
                return _selection.SelectedItem == null
                    ? null
                    : new ItemViewModel(_selection.SelectedItem);
            }
        }

        public ICommand AddItem
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        _selection.SelectedItem = _document.NewItem();
                    });
            }
        }

        public event Func<string, bool> DeletePrompt;

        public ICommand DeleteItem
        {
            get
            {
                return MakeCommand
                    .When(() => _selection.SelectedItem != null)
                    .Do(delegate
                    {
                        if (DeletePrompt == null ||
                            DeletePrompt("Are you sure you want to delete " + _selection.SelectedItem.Name + "?"))
                        {
                            _document.DeleteItem(_selection.SelectedItem);
                            _selection.SelectedItem = null;
                        }
                    });
            }
        }

        public ICommand MoveItemDown
        {
            get
            {
                return MakeCommand
                    .When(() =>
                        _selection.SelectedItem != null &&
                        _document.CanMoveDown(_selection.SelectedItem))
                    .Do(delegate
                    {
                        _document.MoveDown(_selection.SelectedItem);
                    });
            }
        }

        public ICommand MoveItemUp
        {
            get
            {
                return MakeCommand
                    .When(() =>
                        _selection.SelectedItem != null &&
                        _document.CanMoveUp(_selection.SelectedItem))
                    .Do(delegate
                    {
                        _document.MoveUp(_selection.SelectedItem);
                    });
            }
        }
    }
}
