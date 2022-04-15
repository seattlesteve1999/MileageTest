using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MileageManagerForms.DataAccess.Interfaces
{
    public interface IPopupNavigation
    {
        IReadOnlyList<PopupPage> PopupStack { get; }

        Task PopAllAsync(bool animate = true);
        Task PopAsync(bool animate = true);
        Task PushAsync(PopupPage page, bool animate = true);
        Task RemovePageAsync(PopupPage page, bool animate = true);
    }
}
