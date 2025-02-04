using Firebase.Auth;
using GNAutoRota.Models;
using Plugin.FirebaseAuth;

namespace GNAutoRota.ViewPrincipal;

public partial class DashBoard : ContentPage
{
    private readonly FirebaseAuthClient _firebaseAuthClient;
    private readonly IFirebaseAuth _firebaseAuth;

    public DashBoard(FirebaseAuthClient firebaseAuthClient, IFirebaseAuth firebaseAuth)
	{

            _firebaseAuthClient = firebaseAuthClient;
            _firebaseAuth = firebaseAuth;
            InitializeComponent();
            BindingContext = new DashboardViewModel();

    }
}