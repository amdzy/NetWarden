using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace NetManager.Cli.Types;

public class RangeEnabledObservableCollection<T> : ObservableCollection<T>
{
    public void ClearAndAddRange(IEnumerable<T> items)
    {
        this.CheckReentrancy();
        this.Items.Clear();
        foreach (var item in items)
            this.Items.Add(item);
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}