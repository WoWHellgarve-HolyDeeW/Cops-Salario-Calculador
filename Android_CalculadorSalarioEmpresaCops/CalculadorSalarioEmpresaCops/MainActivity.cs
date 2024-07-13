using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using System;

namespace CalculadorSalarioEmpresaCops
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText totalHoursTextBox;
        private EditText nightHoursTextBox;
        private EditText holidayHoursTextBox;
        private Button calculateButton;
        private TextView resultLabel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            totalHoursTextBox = FindViewById<EditText>(Resource.Id.totalHoursTextBox);
            nightHoursTextBox = FindViewById<EditText>(Resource.Id.nightHoursTextBox);
            holidayHoursTextBox = FindViewById<EditText>(Resource.Id.holidayHoursTextBox);
            calculateButton = FindViewById<Button>(Resource.Id.calculateButton);
            resultLabel = FindViewById<TextView>(Resource.Id.resultLabel);

            calculateButton.Click += OnCalculateButtonClick;
        }

        private void OnCalculateButtonClick(object sender, EventArgs e)
        {
            try
            {
                double totalHours = double.Parse(totalHoursTextBox.Text);
                double nightHours = double.Parse(nightHoursTextBox.Text);
                double holidayHours = double.Parse(holidayHoursTextBox.Text);

                var calculador = new CalculadorSalario();
                string resultado = calculador.CalculateSalary(totalHours, nightHours, holidayHours);
                resultLabel.Text = resultado;

                ShowToast("Os cálculos são uma estimativa.");
                ShowToast("Até à data, a margem de erro no salário final conhecida");
                ShowToast("é de 1 a 2 euros.");
            }
            catch (Exception ex)
            {
                ShowToast("Erro ao calcular: " + ex.Message);
            }
        }

        private void ShowToast(string message)
        {
            Toast toast = Toast.MakeText(this, message, ToastLength.Long);
            toast.Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}


