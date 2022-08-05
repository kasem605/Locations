using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Locations.ViewModels;

namespace Locations.Services
{
	public interface INavService
	{
		bool CanGoBack { get; }
		Task GoBack();
		Task NavigateTo<TVM>() where TVM : BaseViewModel;
		Task NavigateTo<TVM, TParameter>(TParameter parameter) where TVM : BaseViewModel;
		void RemoveLastView();
		void ClearBackStack();
		void NavigateUri(Uri uri);

		event PropertyChangedEventHandler CanGoBackChanged;

	}
}

