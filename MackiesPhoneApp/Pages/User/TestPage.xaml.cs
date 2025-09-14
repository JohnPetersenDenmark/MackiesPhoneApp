namespace MackiesPhoneApp.Pages.User;

public partial class TestPage : ContentPage
{
	public TestPage()
	{
		InitializeComponent();
        BindingContext = this;
	}

    private async void OnShowNoShowLocationsTapped(object sender, EventArgs e)
    {
        if (sender is HorizontalStackLayout label)
        {
            var x = 1;
            //var fishShop = GetParentFishShop(label);

            //if (fishShop.IsVisibleTemplateSchedule)
            //{
            //    fishShop.IsVisibleTemplateSchedule = false;
            //    //label.Text = "Vis lokationer";                               
            //}
            //else
            //{
            //    fishShop.IsVisibleTemplateSchedule = true;
            //    // label.Text = "Gem lokationer";
            //}
        }
    }
}