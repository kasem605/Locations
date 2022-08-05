using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Locations.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Reflection;
using Locations.Services;

[assembly: Dependency(typeof(XamarinFormsNavService))]
namespace Locations.Services
{
	public class XamarinFormsNavService : INavService
	{
        readonly IDictionary<Type, Type> map = new Dictionary<Type, Type>();

        public void RegisterViewMapping(Type viewModel, Type view)
        {
            map.Add(viewModel, view);
        }

        public INavigation XamarinFormsNav { get; set; }

        public XamarinFormsNavService()
		{
		}

        public bool CanGoBack => XamarinFormsNav.NavigationStack != null && XamarinFormsNav.NavigationStack.Count > 0;

        public event PropertyChangedEventHandler CanGoBackChanged;

        public void ClearBackStack()
        {
            if(XamarinFormsNav.NavigationStack.Count < 2)
            {
                return;
            }

            for(int i=0;i < XamarinFormsNav.NavigationStack.Count; i++)
            {
                XamarinFormsNav.RemovePage(XamarinFormsNav.NavigationStack[i]);
            }
        }

        public async Task GoBack()
        {
            if (CanGoBack)
            {
                await XamarinFormsNav.PopAsync(true);
                OnCanGoBackChanged();
            }
        }

        public async Task NavigateTo<TVM>() where TVM : BaseViewModel
        {
            await NavigateToView(typeof(TVM));

            if (XamarinFormsNav.NavigationStack.Last().BindingContext is BaseValidationViewModel)
            {
                ((BaseViewModel)XamarinFormsNav.NavigationStack.Last().BindingContext).Init();
            }
        }

        private async Task NavigateToView(Type viewModelType)
        {
            if(!map.TryGetValue(viewModelType, out Type viewType))
            {
                throw new ArgumentException("noviewFound in view mapping for " + viewModelType.FullName + ".");
            }

            //use reflection to get the View's constructor and create an instance of the view

            ConstructorInfo constructor = viewType.GetTypeInfo()
                .DeclaredConstructors.FirstOrDefault(dc => !dc.GetParameters().Any());

            Page view = constructor.Invoke(null) as Page;

            await XamarinFormsNav.PushAsync(view, true);
        }

        void OnCanGoBackChanged() => CanGoBackChanged?.Invoke(this, new PropertyChangedEventArgs("CanGoBack"));

        public async Task NavigateTo<TVM, TParameter>(TParameter parameter) where TVM : BaseViewModel
        {
            await NavigateToView(typeof(TVM));

            if (XamarinFormsNav.NavigationStack.Last().BindingContext is BaseViewModel<TParameter>)
            {
                ((BaseViewModel<TParameter>)XamarinFormsNav.NavigationStack.Last().BindingContext).Init(parameter);
            }
        }

        public void NavigateUri(Uri uri)
        {
            if(uri == null)
            {
                throw new ArgumentException("Invalid URI");
            }

            Device.OpenUri(uri);

        }

        public void RemoveLastView()
        {
            if(XamarinFormsNav.NavigationStack.Count < 2)
            {
                return;
            }

            Page lastView = XamarinFormsNav.NavigationStack[XamarinFormsNav.NavigationStack.Count - 2];

            XamarinFormsNav.RemovePage(lastView);
        }
    }
}

