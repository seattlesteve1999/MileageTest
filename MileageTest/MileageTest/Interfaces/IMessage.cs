using MileageManagerForms.DataAccess;
using Xamarin.Forms.Internals;

namespace MileageManagerForms.Interfaces
{
    public interface IMessage
    {
        void ShowMsg(Mileage value);
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
