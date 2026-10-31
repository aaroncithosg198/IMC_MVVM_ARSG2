using IMC_MVVM_ARSG.ViewModels;

namespace IMC_MVVM_ARSG2.Views;

public partial class ImcPage : ContentPage
{
	public ImcPage()
	{
		InitializeComponent();

		//Conecta la pantalla con sus datos y comandos con el viewmodel
		BindingContext = new ImcViewModel();
	}

}